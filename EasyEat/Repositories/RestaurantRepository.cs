using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyEat.Repositories
{
    public class RestaurantRepository : IRepository<Restaurant>
    {
        private EatContext db;

        public RestaurantRepository()
        {
            this.db = new EatContext();
        }

        public IEnumerable<Restaurant> GetEntityList()
        {
            return db.Restaurant.Include(x => x.Menu);
        }

        public Restaurant GetEntity(object id)
        {
            return db.Restaurant.Include(x => x.Menu).SingleOrDefault(x => x.Id == (int)id);
        }

        public void Create(Restaurant restaurant)
        {
            db.Restaurant.Add(restaurant);
        }

        public void Update(Restaurant restaurant)
        {
            db.Entry(restaurant).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            Restaurant restaurant = db.Restaurant.Find(id);
            if (restaurant != null)
                db.Restaurant.Remove(restaurant);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
