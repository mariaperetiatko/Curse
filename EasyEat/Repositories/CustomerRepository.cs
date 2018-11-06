using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyEat.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private EatContext db;

        public CustomerRepository()
        {
            this.db = new EatContext();
        }

        public IEnumerable<Customer> GetEntityList()
        {
            //return db.Customer.Include(x => x.FoodStyle).Include(x => x.Cart)
            //    .Include(x => x.DeliveryAddress).Include(x => x.FavouriteDish)
            //    .Include(x => x.FoodOrder).Include(x => x.SpecialProduct);
            return db.Customer;
        }

        public Customer GetEntity(object id)
        {
            //return db.Customer.Include(x => x.FoodStyle).Include(x => x.Cart)
            //    .Include(x => x.DeliveryAddress).Include(x => x.FavouriteDish)
            //    .Include(x => x.FoodOrder).Include(x => x.SpecialProduct)
            //    .SingleOrDefault(x => x.Id == (int)id);
            return db.Customer.SingleOrDefault(x => x.Id == (int)id);
        }

        public void Create(Customer customer)
        {
            db.Customer.Add(customer);
        }

        public void Update(Customer customer)
        {
            db.Entry(customer).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            Customer customer = db.Customer.Find(id);
            if (customer != null)
                db.Customer.Remove(customer);
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

