namespace FoodPlace.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string PictureUrl { get; set; }

        public int Size { get; set; }

        public SizeUnit SizeUnit { get; set; }
    }
}