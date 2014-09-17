namespace FoodPlace.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;    
    using System.Linq.Expressions;
   
    using FoodPlace.Models;
    
    public class CartViewModel
    {
        public static Expression<Func<Cart, CartViewModel>> FromCart
        {
            get
            {
                return cart => new CartViewModel
                {
                    Id = cart.Id,
                    UserId = cart.UserId,
                };
            }
        }

        public int Id { get; set; }

        public int UserId { get; set; }
    }
}