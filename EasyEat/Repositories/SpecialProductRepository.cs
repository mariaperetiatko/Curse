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

        public IEnumerable<SpecialProduct> GetWholeEntityList()
        {
            return db.SpecialProduct.Include(x => x.Product).Include(x => x.Customer);
        }

        public Customer GetCustomer(string tokenId)
        {
            Customer customer = db.Customer.Where(x => x.IdentityId == tokenId).FirstOrDefault();
            return customer;
        }

        public IEnumerable<SpecialProduct> GetSpecialProductByCustomer(int id)
        {
            IEnumerable<SpecialProduct> specialProducts = db.SpecialProduct
                .Where(x => x.CustomerId == id);
           
            return specialProducts;
        }

        public SpecialProduct GetEntity(object id)
        {
            SpecialProductKey key = (SpecialProductKey)id;
            //return db.SpecialProduct.Include(x => x.Product).Include(x => x.Customer)
            //    .SingleOrDefault(x => new { x.ProductId, x.CustomerId } == id);
            return db.SpecialProduct.Find(key.ProductId, key.CustomerId);
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
            SpecialProductKey key = (SpecialProductKey)id;
            SpecialProduct specialProduct = db.SpecialProduct
                .Find(key.ProductId, key.CustomerId);
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
