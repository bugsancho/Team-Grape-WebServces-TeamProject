namespace FoodPlace.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using FoodPlace.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

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
            //this.SeedAdminUser(context);
        }
 
        private void SeedAdminUser(FoodPlaceDbContext context)
        {
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

            // Create a RoleStore object by using the ApplicationDbContext object. 
            // The RoleStore is only allowed to contain IdentityRole objects.
            var roleStore = new RoleStore<IdentityRole>(context);

            // Create a RoleManager object that is only allowed to contain IdentityRole objects.
            // When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object. 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            // Then, you create the "canEdit" role if it doesn't already exist.
            if (!roleMgr.RoleExists("admin"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = "admin" });
            }

            // Create a UserManager object based on the UserStore object and the ApplicationDbContext  
            // object. Note that you can create new objects and use them as parameters in
            // a single line of code, rather than using multiple lines of code, as you did
            // for the RoleManager object.
            var userMgr = new UserManager<User>(new UserStore<User>(context));
            var appUser = new User
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com"
            };
            IdUserResult = userMgr.Create(appUser, "admin");

            // If the new "canEdit" user was successfully created, 
            // add the "canEdit" user to the "canEdit" role. 
            if (!userMgr.IsInRole(userMgr.FindByEmail("admin@admin.com").Id, "admin"))
            {
                IdUserResult = userMgr.AddToRole(userMgr.FindByEmail("admin@admin.com").Id, "admin");
            }
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