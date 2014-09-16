namespace FoodPlace.Data
{
    using FoodPlace.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using FoodPlace.Data.Migrations;

    public class FoodPlaceDbContext : IdentityDbContext<User>
    {
        //TODO add sets for other items
        public FoodPlaceDbContext() : base("FoodPlaceConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<FoodPlaceDbContext>());
        }

        public static FoodPlaceDbContext Create()
        {
            return new FoodPlaceDbContext();
        }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Order> Orders { get; set; }
    }
}