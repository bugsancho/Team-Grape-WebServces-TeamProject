namespace FoodPlace.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using FoodPlace.Data;
    using FoodPlace.Web.Models;
    using FoodPlace.Models;
using FoodPlace.Web.Infrastructure;

    [RoutePrefix("api/Categories")]
    public class CategoryController : BaseApiController
    {
        private const string CategoryNotFoundExceptionMassage = "No such ";

        public CategoryController() 
            :this(new FoodPlaceData( new FoodPlaceDbContext()), new AspNetUserIdProvider())
        {
        }

        public CategoryController(IFoodPlaceData data, IUserIdProvider idProvider) : base(data, idProvider)
        {
        }

        //[HttpGet]
        //public IHttpActionResult All()
        //{
        //    var categories = this.data
        //        .Categories
        //        .All()
        //        .Select(CategoryViewModel.FromCategory);

        //    return Ok(categories);
        //}

        [Route("GetCategories")]
        [HttpGet]
        public ICollection<Category> GetCategories() 
        {
            var categories = this.data.Categories.All().ToList();
            return categories;
        }

        [Route("Create")]
        [HttpPost]
        public IHttpActionResult Create(CategoryViewModel category)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCategory = new Category
            {
                Name = category.Name, 
            };

            this.data.Categories.Add(newCategory);
            this.data.Categories.SaveChanges();

            category.Id = newCategory.Id;
            return Ok(newCategory);
        }

        [Route("Read")]
        [HttpGet]
        public ICollection<CategoryViewModel> Read(CategoryViewModel category)
        {
            var categories = this.data.Categories.All().Select(CategoryViewModel.FromCategory).ToList();
            return categories;
        }

        [Route("Update")]
        [HttpPut]
        public IHttpActionResult Update(int id, CategoryViewModel category)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCategory = this.data.Categories.Find(id);
            if (existingCategory == null)
            {
                return BadRequest(CategoryNotFoundExceptionMassage + "category.");
            }

            existingCategory.Name = category.Name;
            existingCategory.Products = category.Products;//not sure if have to update category products

            this.data.Categories.SaveChanges();

            category.Id = id;

            return Ok(existingCategory);
        }

        [Route("Delete")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var existingCategory = this.data.Categories.All().FirstOrDefault(a => a.Id == id);
            if (existingCategory == null)
            {
                return BadRequest(CategoryNotFoundExceptionMassage + "category.");
            }

            this.data.Categories.Delete(existingCategory);
            this.data.Categories.SaveChanges();

            return Ok();
        }

        [Route("AddProduct")]
        [HttpPost]
        public IHttpActionResult AddProduct(int id, int productId)
        {
            var category = this.data.Categories.Find(id);
            if (category == null)
            {
                return BadRequest(CategoryNotFoundExceptionMassage + "category.");
            }

            var product = this.data.Products.Find(id);
            if (product == null)
            {
                return BadRequest(CategoryNotFoundExceptionMassage + "product.");
            }

            category.Products.Add(product);
            this.data.Categories.SaveChanges();

            return Ok(product);
        }
    }
}
