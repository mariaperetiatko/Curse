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

        public IEnumerable<DeliveryAddress> GetEntityList()
        {
            return db.DeliveryAddress;
        }

        public DeliveryAddress GetEntity(object id)
        {
            return db.DeliveryAddress.Find(id);
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
