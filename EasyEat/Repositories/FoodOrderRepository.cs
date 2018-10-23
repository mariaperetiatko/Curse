using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyEat.Repositories
{
    public class FoodOrderRepository : IRepository<FoodOrder>
    {
        private EatContext db;

        public FoodOrderRepository()
        {
            this.db = new EatContext();
        }

        public IEnumerable<FoodOrder> GetEntityList()
        {
            return db.FoodOrder;
        }

        public FoodOrder GetEntity(object id)
        {
            return db.FoodOrder.Find(id);
        }

        public void Create(FoodOrder foodOrder)
        {
            db.FoodOrder.Add(foodOrder);
        }

        public void Update(FoodOrder foodOrder)
        {
            db.Entry(foodOrder).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            FoodOrder foodOrder = db.FoodOrder.Find(id);
            if (foodOrder != null)
                db.FoodOrder.Remove(foodOrder);
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
