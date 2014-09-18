namespace FoodPlace.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using FoodPlace.Data.Repositories;
    using FoodPlace.Models;

    public class FoodPlaceData : IFoodPlaceData 
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public FoodPlaceData()
            : this(new FoodPlaceDbContext())
        {
        }

        public FoodPlaceData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Product> Products
        {
            get
            {
                return this.GetRepository<Product>();
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                return this.GetRepository<Order>();
            }
        }

        public IRepository<Cart> Carts
        {
            get
            {
                return this.GetRepository<Cart>();
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        //public IRepository<User> Users // not sure what User class is doing and if will work here
        //{
        //    get
        //    {
        //        return this.GetRepository<User>();
        //    }
        //}

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}