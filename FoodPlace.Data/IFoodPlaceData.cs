namespace FoodPlace.Data
{
    using FoodPlace.Data.Repositories;
    using FoodPlace.Models;

    public interface IFoodPlaceData
    {
        IRepository<Product> Products { get; }

        IRepository<Order> Orders { get; }

        int SaveChanges();
    }
}