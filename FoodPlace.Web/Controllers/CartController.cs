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

    [RoutePrefix("api/Carts")]
    public class CartController : BaseApiController
    {
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
        public IHttpActionResult AddProduct(int cartId, int productId)
        {
            var cart = this.data.Carts.Find(cartId);
            if (cart == null)
            {
                throw new ArgumentNullException(CartNotFoundExceptionMassage + "card");
            }

            var product = this.data.Products.Find(productId);
            if (product == null)
            {
                throw new ArgumentNullException(CartNotFoundExceptionMassage + "product");
            }

            cart.Products.Add(product);
            this.data.Carts.SaveChanges();

            return Ok(product);
        }

        [Route("RemoveProduct")]
        [HttpPut]//not sure if should be HttpDelete
        public IHttpActionResult RemoveProduct(int cartId, int productId)
        {
            var cart = this.data.Carts.Find(cartId);
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
        public IHttpActionResult BuyCart(int cartId)
        {
            var cart = this.data.Carts.Find(cartId);
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

            return Ok(newOrder);
        }

        [Route("ClearCart")]
        [HttpPost]
        public IHttpActionResult ClearCart(int cartId)
        {
            var cart = this.data.Carts.Find(cartId);
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