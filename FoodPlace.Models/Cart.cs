namespace FoodPlace.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Cart
    {
        private ICollection<Product> products;

        public Cart()
        {
            this.products = new HashSet<Product>();
            this.Id = Guid.NewGuid().ToString();
        }

        [Key, ForeignKey("User")]
        public string Id { get; set; }
        
        public string UserId { get; set; }
        
        [JsonIgnore]
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