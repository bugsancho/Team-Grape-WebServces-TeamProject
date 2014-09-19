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

        public CategoryController() : this(new FoodPlaceData(new FoodPlaceDbContext()), new AspNetUserIdProvider())
        {
        }

        public CategoryController(IFoodPlaceData data, IUserIdProvider idProvider) : base(data, idProvider)
        {
        }

        [Route("GetCategories")]
        [HttpGet]
        public string All()
        {//TODO fix!!!
            try
            {
                var categories = this.data.Categories.All().Select(CategoryViewModel.FromCategory);
              //  return categories;
            }
            catch (Exception ex)
            {
                return ex.Message + ex.StackTrace;
            }
            return "ok";
        }

        [Route("Create")]
        [Authorize(Roles = "admin")]
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

        [Route("Update")]
        [Authorize(Roles = "admin")]
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
            this.data.Categories.SaveChanges();

            category.Id = existingCategory.Id;
            return Ok(existingCategory);
        }

        [Route("Delete")]
        [Authorize(Roles = "admin")]
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
    }
}