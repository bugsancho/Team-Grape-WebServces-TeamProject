using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodPlace.Data;
using FoodPlace.Models;
using System.Web.Http;
using FoodPlace.Web.Infrastructure;

namespace FoodPlace.Web.Controllers
{
    
    public class OrderController : BaseApiController
    {
         public OrderController() :this(new FoodPlaceData( new FoodPlaceDbContext()), new AspNetUserIdProvider())
        {

        }
        public OrderController(IFoodPlaceData data, IUserIdProvider idProvider) : base(data, idProvider)
        {
        }
         [HttpGet]
        public IHttpActionResult Get()
        {
            var product = new Order() { Id = 5, TimeOfOrder = DateTime.Now };
          
            return Ok(product);
        }
    }
}