namespace FoodPlace.Data
{
    using FoodPlace.Data.Repositories;
    using FoodPlace.Models;

    public interface IFoodPlaceData
    {
        IRepository<Product> Products { get; }

        IRepository<Order> Orders { get; }

        IRepository<Cart> Carts { get; }

        IRepository<Category> Categories { get; }

      //  IRepository<User> Users { get; }// mb should be removed

        int SaveChanges();
    }
}