namespace FoodPlace.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string PictureUrl { get; set; }

        public int Size { get; set; }

        public SizeUnit SizeUnit { get; set; }
    }
}