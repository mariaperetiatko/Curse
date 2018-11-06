using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyEat.Repositories
{
    public class DeliveryAddressRepository : IRepository<DeliveryAddress>
    {
        private EatContext db;

        public DeliveryAddressRepository()
        {
            this.db = new EatContext();
        }

        public IEnumerable<DeliveryAddress> GetWholeEntityList()
        {
            return db.DeliveryAddress.Include(x => x.Customer).Include(x => x.Cart)
                .Include(x => x.FoodOrder);
        }

        public IEnumerable<DeliveryAddress> GetEntityList()
        {
            return db.DeliveryAddress;
        }

        public Customer GetCustomer(string tokenId)
        {
            Customer customer = db.Customer.Where(x => x.IdentityId == tokenId).FirstOrDefault();
            return customer;
        }

        public IEnumerable<DeliveryAddress> GetAddressByCustomer(int id)
        {
            IEnumerable<DeliveryAddress> addresses = db.DeliveryAddress
                .Where(x => x.CustomerId == id);
            //addresses.E
            //foreach(Customer customer in addresses.Select(x => x.Customer))
            //{
            //    customer = 
            //}
            return addresses;
        }

        public DeliveryAddress GetEntity(object id)
        {
            return db.DeliveryAddress.Find(id);
        }

        public DeliveryAddress GetWholeEntity(object id)
        {
            return db.DeliveryAddress.Include(x => x.Customer).Include(x => x.Cart)
                .Include(x => x.FoodOrder).SingleOrDefault(x => x.Id == (int)id);
        }

        public void Create(DeliveryAddress deliveryAddress)
        {
            db.DeliveryAddress.Add(deliveryAddress);
        }

        public void Update(DeliveryAddress deliveryAddress)
        {
            db.Entry(deliveryAddress).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            DeliveryAddress deliveryAddress = db.DeliveryAddress.Find(id);
            if (deliveryAddress != null)
                db.DeliveryAddress.Remove(deliveryAddress);
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
