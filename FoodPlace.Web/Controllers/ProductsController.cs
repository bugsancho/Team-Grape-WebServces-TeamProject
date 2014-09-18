using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodPlace.Data;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
namespace FoodPlace.Web.Controllers
{
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
        public string Create()
        {
            var product = new Product() { Name = "pepi", Price = 5.2m, SizeUnit = 0 };
          
            return product.SizeUnit.ToString();
        }
    }
}