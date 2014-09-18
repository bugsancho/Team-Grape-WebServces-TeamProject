namespace FoodPlace.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using FoodPlace.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<FoodPlace.Data.FoodPlaceDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FoodPlace.Data.FoodPlaceDbContext context)
        {
            this.SeedCategories(context);
            this.SeedProducts(context);
        }

        private void SeedProducts(FoodPlaceDbContext context)
        {
            if (context.Products.Any())
            {
                return;
            }

            context.Products.Add(new Product
                {
                    Name = "Mexicana",
                    Price = 7.46m,
                    Description = "tomato sauce, ham, peppers, beans, corn, onion",
                    CategoryId = 1,
                    Size = 500,
                    SizeUnit = SizeUnit.Grams
                });

            context.Products.Add(new Product
            {
                Name = "Calzone",
                Price = 5.56m,
                Description = "tomato sauce, ham, mushrooms, cheese, olives, onion",
                CategoryId = 1,
                Size = 450,
                SizeUnit = SizeUnit.Grams
            });

            context.Products.Add(new Product
            {
                Name = "Capricciosa",
                Price = 9.50m,
                Description = "tomato sauce, bacon, ham, mushrooms, olives, fresh tomatos",
                CategoryId = 1,
                Size = 700,
                SizeUnit = SizeUnit.Grams
            });

            context.Products.Add(new Product
            {
                Name = "Mexicana",
                Price = 7.46m,
                Description = "tomato sauce, ham, peppers, beans, corn, onion",
                CategoryId = 1,
                Size = 500,
                SizeUnit = SizeUnit.Grams
            });

            context.Products.Add(new Product
            {
                Name = "Mexicana",
                Price = 7.46m,
                Description = "tomatos, cucumbers, peppers, beans, corn, onion",
                CategoryId = 6,
                Size = 500,
                SizeUnit = SizeUnit.Grams
            });

            context.Products.Add(new Product
            {
                Name = "Italian",
                Price = 7.46m,
                Description = "tomatos, potatoes, ham, chees, cucumbers, onion",
                CategoryId = 6,
                Size = 500,
                SizeUnit = SizeUnit.Grams
            });
        }

        private void SeedCategories(FoodPlaceDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            context.Categories.Add(new Category
            {
                Name = "pizza"
            });

            context.Categories.Add(new Category
            {
                Name = "pasta"
            });

            context.Categories.Add(new Category
            {
                Name = "alcohol"
            });

            context.Categories.Add(new Category
            {
                Name = "soft drink"
            });

            context.Categories.Add(new Category
            {
                Name = "dessert"
            });


            context.Categories.Add(new Category
            {
                Name = "salad"
            });
        }
    }
}
