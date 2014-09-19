namespace FoodPlace.RestApi.Tests
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Telerik.JustMock;
    using FoodPlace.Data;
    using FoodPlace.Data.Repositories;
    using FoodPlace.Models;
    using FoodPlace.Web.Controllers;
    using FoodPlace.Web.Infrastructure;
    using FoodPlace.Web.Models;

    [TestClass]
    public class ProductControllerTests
    {
        // Doesn't work!!!!
        //[TestMethod]
        //public void TestMethod1()
        //{
        //    var repo = Mock.Create<IRepository<Product>>();
        //    Product[] products = GetProducts();
        //    Mock.Arrange(() => repo.All())
        //        .Returns(() => products.AsQueryable<Product>());

        //    var data = Mock.Create<IFoodPlaceData>();
        //    Mock.Arrange(() => data.Products)
        //        .Returns(() => repo);

        //    var provider = Mock.Create<IUserIdProvider>();
        //    Mock.Arrange(() => provider.GetUserId())
        //        .Returns(() => "pesho");

        //    var pr = data.Products.All();
        //    var controller = new ProductsController(data, provider);
        //    var result = controller.GetByName("Mexicana").ToList<ProductModel>();

        //    foreach (var product in result)
        //    {
        //        Assert.AreEqual("Mexicana", product.Name);
        //    }
        //}

        private Product[] GetProducts()
        {
            Product[] products = 
            {
                new Product
                {
                    Name = "Mexicana",
                    Price = 7.46m,
                    Description = "tomato sauce, ham, peppers, beans, corn, onion",
                    CategoryId = 1,
                    Size = 500,
                    SizeUnit = SizeUnit.Grams
                },
                new Product
                {
                    Name = "Calzone",
                    Price = 5.56m,
                    Description = "tomato sauce, ham, mushrooms, cheese, olives, onion",
                    CategoryId = 1,
                    Size = 450,
                    SizeUnit = SizeUnit.Grams
                },
                new Product
                {
                    Name = "Capricciosa",
                    Price = 9.50m,
                    Description = "tomato sauce, bacon, ham, mushrooms, olives, fresh tomatos",
                    CategoryId = 1,
                    Size = 700,
                    SizeUnit = SizeUnit.Grams
                },
                new Product
                {
                    Name = "Mexicana",
                    Price = 7.46m,
                    Description = "tomatos, cucumbers, peppers, beans, corn, onion",
                    CategoryId = 6,
                    Size = 500,
                    SizeUnit = SizeUnit.Grams
                },
                new Product
                {
                    Name = "Italian",
                    Price = 7.46m,
                    Description = "tomatos, potatoes, ham, chees, cucumbers, onion",
                    CategoryId = 6,
                    Size = 500,
                    SizeUnit = SizeUnit.Grams
                }
            };

            return products;
        }
    }
}