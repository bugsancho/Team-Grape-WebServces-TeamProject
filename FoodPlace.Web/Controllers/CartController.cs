namespace FoodPlace.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using FoodPlace.Data;
    using FoodPlace.Models;
    using FoodPlace.Web.Infrastructure;
    using Newtonsoft.Json;

    [RoutePrefix("api/Carts")]
    public class CartController : BaseApiController
    {
        static PubnubAPI pubnub = new PubnubAPI(
               "pub-c-02dee9ae-9627-4fb7-a486-a5cdf9df9749",               // PUBLISH_KEY
               "sub-c-04a5c868-0374-11e3-bde1-02ee2ddab7fe",               // SUBSCRIBE_KEY
               "sec-c-NTUzNGMyMjgtNDhkZS00ZTMwLWEyYTEtNGY5ZjYyOGU5ODY2",   // SECRET_KEY
               true                                                        // SSL_ON?
           );

        private const string CartNotFoundExceptionMassage = "No such ";
 
        public CartController() : this(new FoodPlaceData(new FoodPlaceDbContext()), new AspNetUserIdProvider())
        {
        }

        public CartController(IFoodPlaceData data, IUserIdProvider idProvider) : base(data, idProvider)
        {
        }

        [Authorize]
        [Route("Get")]
        [HttpGet]
        public IHttpActionResult GetCartId()
        {
            var userId = this.userIdProvider.GetUserId();
            var user = this.data.Users.Find(userId);
            if (user.Cart == null)
            {
                user.Cart = new Cart();
            }
            this.data.SaveChanges(); 
            return Ok(user.Cart.Id);
        }

        [Route("AddProduct")]
        [HttpPut]//or mb post
        public IHttpActionResult AddProduct(int productId)
        {
            var cart = this.data.Carts.Find(this.GetCartId());
            if (cart == null)
            {
                BadRequest(CartNotFoundExceptionMassage + "card");
            }

            var product = this.data.Products.Find(productId);
            if (product == null)
            {
                BadRequest(CartNotFoundExceptionMassage + "product");
            }

            cart.Products.Add(product);
            this.data.Carts.SaveChanges();

            return Ok(product);
        }

        [Route("RemoveProduct")]
        [HttpPut]//not sure if should be HttpDelete
        public IHttpActionResult RemoveProduct(int productId)
        {
            var cart = this.data.Carts.Find(this.GetCartId());
            if (cart == null)
            {
                throw new ArgumentNullException(CartNotFoundExceptionMassage + "card");
            }

            var product = this.data.Products.Find(productId);
            if (product == null)
            {
                throw new ArgumentNullException(CartNotFoundExceptionMassage + "product");
            }

            cart.Products.Remove(product);
            this.data.Carts.SaveChanges();

            return Ok(product);
        }

        [Route("BuyCart")]
        [HttpPost]
        public IHttpActionResult BuyCart()
        {
            var cart = this.data.Carts.Find(this.GetCartId());
            if (cart == null)
            {
                throw new ArgumentNullException(CartNotFoundExceptionMassage + "card");
            }

            var newOrder = new Order
            {
                Products = cart.Products,
                TimeOfOrder = DateTime.Now,
                UserId = cart.UserId,
            };

            this.data.Orders.Add(newOrder);
            this.data.Orders.SaveChanges();

            string orderAsJson = JsonConvert.SerializeObject(newOrder);

            pubnub.Publish("admin", orderAsJson);
            return Ok(newOrder);
        }

        [Route("ClearCart")]
        [HttpPost]
        public IHttpActionResult ClearCart()
        {
            var cart = this.data.Carts.Find(this.GetCartId());
            if (cart == null)
            {
                throw new ArgumentNullException(CartNotFoundExceptionMassage + "card");
            }

            cart.Products.Clear();
            this.data.Carts.SaveChanges();

            return Ok();
        }

        
    }
}