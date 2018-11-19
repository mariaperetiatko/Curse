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
            return db.FoodStyleProduct;
        }

        public IEnumerable<FoodStyleProduct> GetWholeEntityList()
        {
            return db.FoodStyleProduct.Include(x => x.FoodStyle).Include(x => x.Product);
        }

        public FoodStyleProduct GetEntity(object id)
        {
            FoodStyleProductKey key = (FoodStyleProductKey)id;
            //return db.FoodStyleProduct.Include(x => x.FoodStyle).Include(x => x.Product)
            //    .SingleOrDefault(x => new { x.FoodStyleId, x.ProductId } == id);
            return db.FoodStyleProduct.Find(key.FoodStyleId, key.ProductId);
        }

        public FoodStyleProduct GetWholeEntity(object id)
        {
            FoodStyleProductKey key = (FoodStyleProductKey)id;
       
            return db.FoodStyleProduct.Include(x => x.FoodStyle).Include(x => x.Product)
                  .SingleOrDefault(x => x.FoodStyleId == key.FoodStyleId
                  & x.ProductId == key.ProductId);
        }

        public List<Product> GetProductsByFoodStyle(int foodStyleId)
        {
            List<Product> products = new List<Product>();
            List<int> productIds = db.FoodStyleProduct.Where(x => x.FoodStyleId == foodStyleId).
                Select(y => y.ProductId).ToList();
            for(int i = 0; i < productIds.Count(); i++)
            {
                products.AddRange(db.Product.Where(x => x.Id == productIds[i]));
            }
            return products;

        }

        public void Create(FoodStyleProduct foodStyleProduct)
        {
            FoodStyleProduct prbableFoodStyleProduct = db.FoodStyleProduct
                .Find(foodStyleProduct.FoodStyleId, foodStyleProduct.ProductId);
            if (prbableFoodStyleProduct == null)
                db.FoodStyleProduct.Add(foodStyleProduct);
        }

        public void Update(FoodStyleProduct foodStyleProduct)
        {
            db.Entry(foodStyleProduct).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            FoodStyleProductKey key = (FoodStyleProductKey)id;
            FoodStyleProduct foodStyleProduct = db.FoodStyleProduct
                .Find(key.FoodStyleId, key.ProductId);
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
