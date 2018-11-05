using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyEat.Repositories
{
    public class FoodStyleProductRepository : IRepository<FoodStyleProduct>
    {
        private EatContext db;

        public FoodStyleProductRepository()
        {
            this.db = new EatContext();
        }

        public IEnumerable<FoodStyleProduct> GetEntityList()
        {
            return db.FoodStyleProduct.Include(x => x.FoodStyle).Include(x => x.Product);
        }

        public FoodStyleProduct GetEntity(object id)
        {
            return db.FoodStyleProduct.Include(x => x.FoodStyle).Include(x => x.Product)
                .SingleOrDefault(x => new { x.FoodStyleId, x.ProductId } == id);
        }

        public void Create(FoodStyleProduct foodStyleProduct)
        {
            db.FoodStyleProduct.Add(foodStyleProduct);
        }

        public void Update(FoodStyleProduct foodStyleProduct)
        {
            db.Entry(foodStyleProduct).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            FoodStyleProduct foodStyleProduct = db.FoodStyleProduct.Find(id);
            if (foodStyleProduct != null)
                db.FoodStyleProduct.Remove(foodStyleProduct);
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
