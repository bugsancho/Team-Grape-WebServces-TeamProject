using FoodPlace.Data;
using FoodPlace.Web.Infrastructure;
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
        protected IUserIdProvider userIdProvider;

        protected BaseApiController(IFoodPlaceData data, IUserIdProvider idProvider)
        {
            this.data = data;
            this.userIdProvider = idProvider;
        }
        protected BaseApiController()
        {

        }
    }
}