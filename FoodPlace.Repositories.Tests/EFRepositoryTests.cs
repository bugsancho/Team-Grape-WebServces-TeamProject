namespace FoodPlace.Repositories.Tests
{
    using System;
    using System.Data.Entity;
    using System.Transactions;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FoodPlace.Data;
    using FoodPlace.Data.Repositories;
    using FoodPlace.Models;

    [TestClass]
    public class EFRepositoryTests
    {
        static TransactionScope tran;

        [TestInitialize]
        public void TestInit()
        {
            tran = new TransactionScope();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void AddCategory_WhenValid_ShouldAddToDb()
        {
            var category = new Category
            {
                Name = "TestCategory"
            };

            var dbContext = new FoodPlaceDbContext();
            var repo = new EFRepository<Category>(dbContext);

            repo.Add(category);
            repo.SaveChanges();

            var categoryInDb = dbContext.Categories.Find(category.Id);

            Assert.IsNotNull(categoryInDb);
            Assert.AreEqual(category.Name, categoryInDb.Name);
        }

        //[TestMethod]
        //public void AddProduct_WhenValid_ShouldAddToDb()
        //{
        //    var category = new Category();
        //    var product = new Product
        //    {
        //        Name = "TestProduct",
        //        Price = 2.5m,
        //        Description = "Some product for testing",
        //        PictureUrl = "someurl",
        //        Size = 200,
        //        SizeUnit = SizeUnit.Grams
        //    };

        //    var dbContext = new FoodPlaceDbContext();
        //    var repo = new EFRepository<Product>(dbContext);

        //    repo.Add(product);
        //    repo.SaveChanges();

        //    var productInDb = dbContext.Products.Find(product.Id);

        //    Assert.IsNotNull(productInDb);
        //    Assert.AreEqual(product.Name, productInDb.Name);
        //    Assert.AreEqual(product.Price, productInDb.Price);
        //    Assert.AreEqual(product.Description, productInDb.Description);
        //    Assert.AreEqual(product.CategoryId, productInDb.CategoryId);
        //    Assert.AreEqual(product.PictureUrl, productInDb.PictureUrl);
        //    Assert.AreEqual(product.Size, productInDb.Size);
        //    Assert.AreEqual(product.SizeUnit, productInDb.SizeUnit);
        //}

        [TestMethod]
        public void Find_WhenObjectIsInDb_ShouldReturnTheObject()
        {
            var category = new Category
            {
                Name = "TestCategory"
            };

            var dbContext = new FoodPlaceDbContext();
            var repo = new EFRepository<Category>(dbContext);

            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            var categoryInDb = repo.Find(category.Id);

            Assert.IsNotNull(categoryInDb);
            Assert.AreEqual(category.Name, categoryInDb.Name);
        }

        [TestMethod]
        public void Delete_WhenExistingObject_ShouldRemoveTheObjectFromDb()
        {
            var category = new Category
            {
                Name = "TestCategory"
            };

            var dbContext = new FoodPlaceDbContext();
            var repo = new EFRepository<Category>(dbContext);

            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            var categoryInDb = dbContext.Categories.Find(category.Id);
            var oldCount = dbContext.Categories.Count();
            var deletedCategory = repo.Delete(categoryInDb);
            repo.SaveChanges();

            var newCount = dbContext.Categories.Count();
            Assert.AreEqual(oldCount - 1, newCount);
            Assert.AreEqual(categoryInDb.Name, deletedCategory.Name);
        }

        [TestMethod]
        public void Update_WhenExistingObject_ShouldChangeTheObject()
        {
            var category = new Category
            {
                Name = "TestCategory"
            };

            var dbContext = new FoodPlaceDbContext();
            var repo = new EFRepository<Category>(dbContext);

            dbContext.Categories.Add(category);
            dbContext.SaveChanges();

            var categoryInDb = dbContext.Categories.Find(category.Id);
            categoryInDb.Name = "ChangedName";
            repo.Update(categoryInDb);
            dbContext.SaveChanges();

            categoryInDb = dbContext.Categories.Find(category.Id);
            Assert.AreEqual("ChangedName", categoryInDb.Name);
        }
    }
}