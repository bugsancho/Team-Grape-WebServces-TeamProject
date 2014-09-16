using FoodPlace.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FoodPlace.Web.Controllers
{
   // [Authorize]
    public abstract class BaseApiController : ApiController
    {
        protected IFoodPlaceData data;

        protected BaseApiController(IFoodPlaceData data)
        {
            this.data = data;
        }
    }
}