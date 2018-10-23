using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using Microsoft.EntityFrameworkCore;


namespace EasyEat.Repositories
{
    public class DishRepository : IRepository<Dish>
    {
        private EatContext db;

        public DishRepository()
        {
            this.db = new EatContext();
        }

        public IEnumerable<Dish> GetEntityList()
        {
            return db.Dish;
        }

        public Dish GetEntity(object id)
        {
            return db.Dish.Find(id);
        }

        public void Create(Dish dish)
        {
            db.Dish.Add(dish);
        }

        public void Update(Dish dish)
        {
            db.Entry(dish).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            Dish dish = db.Dish.Find(id);
            if (dish != null)
                db.Dish.Remove(dish);
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
