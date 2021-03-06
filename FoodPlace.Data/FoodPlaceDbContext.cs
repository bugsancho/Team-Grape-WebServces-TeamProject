﻿namespace FoodPlace.Data
{
    using FoodPlace.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using FoodPlace.Data.Migrations;

    public class FoodPlaceDbContext : IdentityDbContext<User>
    {
        public FoodPlaceDbContext()
            : base("FoodPlaceConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<FoodPlaceDbContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FoodPlaceDbContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //            .HasOptional(f => f.Cart)
            //            .WithRequired(s => s.User);
            base.OnModelCreating(modelBuilder);
        }

        public static FoodPlaceDbContext Create()
        {
            return new FoodPlaceDbContext();
        }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Order> Orders { get; set; }

        public IDbSet<Category> Categories { get; set; }

        //public IDbSet<Cart> Carts { get; set; }
    }
}