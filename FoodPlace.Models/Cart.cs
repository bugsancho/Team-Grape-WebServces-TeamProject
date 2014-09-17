namespace FoodPlace.Models
{
    using System;
    using System.Collections.Generic;

    public class Cart
    {
        private ICollection<Product> products;

        public Cart()
        {
            this.products = new HashSet<Product>();
        }

        public int Id { get; set; }
        
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Product> Products
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