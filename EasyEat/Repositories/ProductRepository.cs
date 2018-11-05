using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyEat.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private EatContext db;

        public ProductRepository()
        {
            this.db = new EatContext();
        }

        public IEnumerable<Product> GetEntityList()
        {
            return db.Product.Include(x => x.FoodStyleProduct).Include(x => x.Ingredient)
                .Include(x => x.SpecialProduct);
        }

        public Product GetEntity(object id)
        {
            return db.Product.Include(x => x.FoodStyleProduct).Include(x => x.Ingredient)
                .Include(x => x.SpecialProduct).SingleOrDefault(x => x.Id == (int)id);
        }

        public void Create(Product product)
        {
            db.Product.Add(product);
        }

        public void Update(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            Product product = db.Product.Find(id);
            if (product != null)
                db.Product.Remove(product);
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
