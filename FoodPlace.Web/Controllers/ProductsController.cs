namespace FoodPlace.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using FoodPlace.Web.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using FoodPlace.Data;
    using FoodPlace.Models;
    using FoodPlace.Web.Infrastructure;
    using System.Net;

    public class ProductsController : BaseApiController
    {
        public ProductsController() : this(new FoodPlaceData(new FoodPlaceDbContext()), new AspNetUserIdProvider())
        {
        }

        public ProductsController(IFoodPlaceData data, IUserIdProvider userIdProvider) : base(data, userIdProvider)
        {
        }

        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            var product = this.data
            .Products
                              .All()
                              .Where(p => p.Id == id)
                              .Select(ProductModel.FromProduct)
                              .FirstOrDefault();

            if (product == null)
            {
                return BadRequest("Product does not exist - invalid id");
            }

            return Ok(product);
        }

        [HttpGet]
        public IQueryable<ProductModel> GetByCategory(int categoryId)
        {
            var products = this.data
            .Products
                               .All()
                               .Where(p => p.CategoryId == categoryId)
                               .Select(ProductModel.FromProduct);

            return products.AsQueryable<ProductModel>();
        }

        [HttpGet]
        public IQueryable<ProductModel> GetByName(string name)
        {
            var products = this.data
            .Products
                               .All()
                               .Where(p => p.Name == name)
                               .Select(ProductModel.FromProduct);
            
            return products.AsQueryable<ProductModel>();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IHttpActionResult CreateProduct(ProductModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = this.data.Categories.All().Where(c => c.Name == product.Category).FirstOrDefault();
            if (category == null)
            {
                return BadRequest("Invalid category");
            }

            Product newProduct = new Product()
            {
                Description = product.Description,
                Name = product.Name,
                Category = category,
                Price = product.Price,
                Size = product.Size,
                SizeUnit = (SizeUnit)Enum.Parse(typeof(SizeUnit), product.SizeUnit)
            };

            this.data.Products.Add(newProduct);
            this.data.SaveChanges();

            product.Id = newProduct.Id;
            return Ok(product);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public IHttpActionResult Update(int id, ProductModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = this.data.Products.All().FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return BadRequest("Such product doesn't exist!");
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Size = product.Size;
            var category = this.data.Categories.All().Where(c => c.Name == c.Name).FirstOrDefault();
            if (category == null)
            {
                return BadRequest("Invalid category");
            }

            existingProduct.Category = category;
            existingProduct.SizeUnit = (SizeUnit)Enum.Parse(typeof(SizeUnit), product.SizeUnit);
            this.data.SaveChanges();

            product.Id = id;
            return Ok(product);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public IHttpActionResult ChangeProductName(int id, string newName)
        {
            var oldProductQuery = this.data.Products.All().Where(p => p.Id == id);
            if (oldProductQuery == null)
            {
                return BadRequest("Such product does not exist");
            }

            oldProductQuery.FirstOrDefault().Name = newName;
            this.data.SaveChanges();

            return Ok(oldProductQuery.Select(ProductModel.FromProduct));
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public IHttpActionResult ChangeProductCategory(int id, int newCategoryId)
        {
            var productQuery = this.data.Products.All().Where(p => p.Id == id);
            if (productQuery == null)
            {
                return BadRequest("Such product does not exist!");
            }

            var category = this.data.Categories.All().Where(c => c.Id == newCategoryId).FirstOrDefault();
            if (category == null)
            {
                return BadRequest("Such category does not exist!");
            }

            productQuery.FirstOrDefault().Category = category;
            this.data.SaveChanges();

            return Ok(productQuery.Select(ProductModel.FromProduct));
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public IHttpActionResult ChangeProductDescription(int id, string newDescription)
        {
            var oldProductQuery = this.data.Products.All().Where(p => p.Id == id);
            if (oldProductQuery == null)
            {
                return BadRequest("Such product does not exist!");
            }

            oldProductQuery.FirstOrDefault().Description = newDescription;
            this.data.SaveChanges();

            return Ok(oldProductQuery.Select(ProductModel.FromProduct));
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public IHttpActionResult ChangeProductPrice(int id, decimal newPrice)
        {
            var oldProductQuery = this.data.Products.All().Where(p => p.Id == id);
            if (oldProductQuery == null)
            {
                return BadRequest("Such product does not exist!");
            }

            oldProductQuery.FirstOrDefault().Price = newPrice;
            this.data.SaveChanges();

            return Ok(oldProductQuery.Select(ProductModel.FromProduct));
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public IHttpActionResult ChangeProductPicture(int id, string newPictureUrl)
        {
            var oldProductQuery = this.data.Products.All().Where(p => p.Id == id);
            if (oldProductQuery == null)
            {
                return BadRequest("Such product does not exist!");
            }

            oldProductQuery.FirstOrDefault().PictureUrl = newPictureUrl;
            this.data.SaveChanges();

            return Ok(oldProductQuery.Select(ProductModel.FromProduct));
        }
            
        [HttpPut]
        [Authorize(Roles = "admin")]
        public IHttpActionResult ChangeProductSize(int id, int newSize)
        {
            var oldProductQuery = this.data.Products.All().Where(p => p.Id == id);
            if (oldProductQuery == null)
            {
                return BadRequest("Such product does not exist!");
            }

            oldProductQuery.FirstOrDefault().Size = newSize;
            this.data.SaveChanges();

            return Ok(oldProductQuery.Select(ProductModel.FromProduct));
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public IHttpActionResult DeleteProduct(int id)
        {
            var product = this.data.Products.All().Where(p => p.Id == id).FirstOrDefault();
            if (product == null)
            {
                return BadRequest("Such product doesn't exist");
            }

            this.data.Products.Delete(product);
            this.data.SaveChanges();

            return Ok(product);
        }
    }
}