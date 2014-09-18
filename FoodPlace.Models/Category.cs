﻿namespace FoodPlace.Models
{
    using System.Collections.Generic;

    public class Category
    {
        private ICollection<Product> products;

        public int Id { get; set; }

        public string Name { get; set; }

        public Category()
        {
            this.products = new HashSet<Product>();
        }

        public ICollection<Product> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                this.products = value;
            }
        }
    }
}