namespace FoodPlace.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using FoodPlace.Web.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;

    using FoodPlace.Data;
    using FoodPlace.Models;
    using FoodPlace.Web.Infrastructure;

    public class ProductsController : BaseApiController
    {
        private IUserIdProvider userIdProvider;

        public ProductsController()
            : this(new FoodPlaceData(new FoodPlaceDbContext()), new AspNetUserIdProvider())
        {
        }

        public ProductsController(IFoodPlaceData data, IUserIdProvider userIdProvider)
            : base(data)
        {
            this.userIdProvider = userIdProvider;
        }

        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            var product = this.data
                .Products
                .All()
                .Where(p => p.Id == id)
                .Select(ProductModel.FromProduct)
                .FirstOrDefault();

            if (product == null)
            {
                return BadRequest("Product does not exist - invalid id");
            }

            return Ok(product);
        }

        [HttpGet]
        public IHttpActionResult GetByCategory(int categoryId)
        {
            var products = this.data
                .Products
                .All()
                .Where(p => p.CategoryId == categoryId)
                .Select(ProductModel.FromProduct);

            if (products.Count() == 0)
            {
                return BadRequest("There are no products in this category");
            }

            return Ok(products);
        }

        [HttpGet]
        public IHttpActionResult GetByName(string name)
        {
            var products = this.data
                .Products
                .All()
                .Where(p => p.Name == name)
                .Select(ProductModel.FromProduct);

            if(products.Count() == 0)
            {
                return BadRequest("There are no products with this name");
            }

            return Ok(products);
        }
    }
}