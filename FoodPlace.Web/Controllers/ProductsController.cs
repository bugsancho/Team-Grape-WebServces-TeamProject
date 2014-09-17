using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodPlace.Data;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using FoodPlace.Models;

namespace FoodPlace.Web.Controllers
{
    public class ProductsController : BaseApiController
    {
        public ProductsController() : this(new FoodPlaceData(new FoodPlaceDbContext()))
        {
        }

        public ProductsController(IFoodPlaceData data) : base(data)
        {
        }

        [HttpGet]
        public string Get()
        {
            var product = new Product() { Name = "pepi", Price = 5.2m, SizeUnit = 0 };
          


            return product.SizeUnit.ToString();
        }
    }
}