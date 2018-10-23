using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyEat.Repositories
{
    public class SpecialProductRepository : IRepository<SpecialProduct>
    {
        private EatContext db;

        public SpecialProductRepository()
        {
            this.db = new EatContext();
        }

        public IEnumerable<SpecialProduct> GetEntityList()
        {
            return db.SpecialProduct;
        }

        public SpecialProduct GetEntity(object id)
        {
            return db.SpecialProduct.Find(id);
        }

        public void Create(SpecialProduct specialProduct)
        {
            db.SpecialProduct.Add(specialProduct);
        }

        public void Update(SpecialProduct specialProduct)
        {
            db.Entry(specialProduct).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            SpecialProduct specialProduct = db.SpecialProduct.Find(id);
            if (specialProduct != null)
                db.SpecialProduct.Remove(specialProduct);
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
