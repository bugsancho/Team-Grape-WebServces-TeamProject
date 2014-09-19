namespace FoodPlace.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using FoodPlace.Models;

    public class ProductModel
    {
        public static Expression<Func<Product, ProductModel>> FromProduct
        {
            get
            {
                return p => new ProductModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    Category = p.Category.Name,
                    Size = p.Size,
                    SizeUnit = p.SizeUnit.ToString(),
                    PictureUrl = p.PictureUrl
                };
            }
        }

        public int Id { get; set; }
        
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public int Size { get; set; }

        public string SizeUnit { get; set; }

        public string PictureUrl { get; set; }     
    }
}