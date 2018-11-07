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
            return db.FoodOrder.Include(x => x.Address).Include(x => x.Customer);
        }
    
        public FoodOrder GetEntity(object id)
        {
            return db.FoodOrder.Include(x => x.Address).Include(x => x.Customer)
                .SingleOrDefault(x => x.Id == (int)id);
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

        public Customer GetCustomer(string tokenId)
        {
            Customer customer = db.Customer.Where(x => x.IdentityId == tokenId).FirstOrDefault();
            return customer;
        }

        public IEnumerable<FoodOrder> GetFoodOrderByCustomer(int id)
        {
            IEnumerable<FoodOrder> foodOrder = db.FoodOrder
                .Where(x => x.CustomerId == id);

            return foodOrder;
        }

        public Cart GetCart(FoodOrder foodOrder)
        {
            return db.Cart.FirstOrDefault(x => x.CustomerId == foodOrder.CustomerId);
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
