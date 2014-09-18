namespace FoodPlace.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Linq.Expressions;

    using FoodPlace.Models;

    public class CategoryViewModel
    {
        public static Expression<Func<Category, CategoryViewModel>> FromCategory
        {
            get
            {
                return category => new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}