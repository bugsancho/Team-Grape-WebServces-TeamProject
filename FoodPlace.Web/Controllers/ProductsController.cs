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
        

        public ProductsController()
            : this(new FoodPlaceData(new FoodPlaceDbContext()), new AspNetUserIdProvider())
        {
        }

        public ProductsController(IFoodPlaceData data, IUserIdProvider userIdProvider)
            : base(data, userIdProvider)
        {
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

        [HttpPost]
        public IHttpActionResult CreateProduct(ProductModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Can't get the category without making a request to the db, because the model uses string for Category
            // The product model does not have pictureUrl it can't be added to the database
            Product newProduct = new Product()
            {
                Description = product.Description,
                Name = product.Name,
                Category = this.data.Categories.All().Where(c => c.Name == product).FirstOrDefault(),
                Price = product.Price,
                Size = product.Size,
                SizeUnit = (SizeUnit)Enum.Parse(typeof(SizeUnit), product.SizeUnit)                 
            };

            this.data.Products.Add(newProduct);
            this.data.SaveChanges();

            return Ok(product);
        }

        [HttpPut]
        public IHttpActionResult ChangeProductName(int id, string newName)
        {
            var oldProductQuery = this.data.Products.All().Where(p => p.Id == id);
            if (oldProductQuery == null)
            {
                return NotFound();
            }

            oldProductQuery.FirstOrDefault().Name = newName;
            this.data.SaveChanges();

            return Ok(oldProductQuery.Select(ProductModel.FromProduct));
        }

        [HttpPut]
        public IHttpActionResult ChangeProductCategory(int id, int newCategoryId)
        {
            var productQuery = this.data.Products.All().Where(p => p.Id == id);
            if (productQuery == null)
            {
                return NotFound();
            }

            var category = this.data.Categories.All().Where(c => c.Id == newCategoryId).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }

            productQuery.FirstOrDefault().Category = category;
            this.data.SaveChanges();
            
            return Ok(productQuery.Select(ProductModel.FromProduct));
        }

        [HttpPut]
        public IHttpActionResult ChangeProductDescription(int id, string newDescription)
        {
            var oldProductQuery = this.data.Products.All().Where(p => p.Id == id);
            if (oldProductQuery == null)
            {
                return NotFound();
            }

            oldProductQuery.FirstOrDefault().Description = newDescription;
            this.data.SaveChanges();

            return Ok(oldProductQuery.Select(ProductModel.FromProduct));
        }

        [HttpPut]
        public IHttpActionResult ChangeProductPrice(int id, decimal newPrice)
        {
            var oldProductQuery = this.data.Products.All().Where(p => p.Id == id);
            if (oldProductQuery == null)
            {
                return NotFound();
            }

            oldProductQuery.FirstOrDefault().Price = newPrice;
            this.data.SaveChanges();

            return Ok(oldProductQuery.Select(ProductModel.FromProduct));
        }

        [HttpPut]
        public IHttpActionResult ChangeProductPicture(int id, string newPictureUrl)
        {
            var oldProductQuery = this.data.Products.All().Where(p => p.Id == id);
            if (oldProductQuery == null)
            {
                return NotFound();
            }

            oldProductQuery.FirstOrDefault().PictureUrl = newPictureUrl;
            this.data.SaveChanges();

            return Ok(oldProductQuery.Select(ProductModel.FromProduct));
        }

        [HttpPut]
        public IHttpActionResult ChangeProductSize(int id, int newSize)
        {
            var oldProductQuery = this.data.Products.All().Where(p => p.Id == id);
            if (oldProductQuery == null)
            {
                return NotFound();
            }

            oldProductQuery.FirstOrDefault().Size = newSize;
            this.data.SaveChanges();

            return Ok(oldProductQuery.Select(ProductModel.FromProduct));
        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var product = this.data.Products.All().Where(p => p.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }

            this.data.Products.Delete(product);
            this.data.SaveChanges();

            return Ok(product);
        }
    }
}