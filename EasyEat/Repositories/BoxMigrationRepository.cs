using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyEat.Repositories
{
    public class BoxMigrationRepository : IRepository<BoxMigration>
    {

        private EatContext db;

        public BoxMigrationRepository()
        {
            this.db = new EatContext();
        }

        public IEnumerable<BoxMigration> GetEntityList()
        {
            return db.BoxMigration;
        }

        public IEnumerable<BoxMigration> GetWholeEntityList()
        {
            return db.BoxMigration.Include(x => x.FoodOrder);
        }

        public BoxMigration GetEntity(object id)
        {      
            return db.BoxMigration.Find((int)id);
        }

        public BoxMigration GetWholeEntity(object id)
        { 
            return db.BoxMigration.Include(x => x.FoodOrder).SingleOrDefault(x => x.Id == (int)id);
        }

        public Customer GetCustomer(string tokenId)
        {
            Customer customer = db.Customer.Where(x => x.IdentityId == tokenId).FirstOrDefault();
            return customer;
        }

        public IEnumerable<BoxMigration> GetBoxMigrationByFoodOrder(int orderId)
        {
            IEnumerable<BoxMigration> boxMigration = db.BoxMigration
                .Where(x => x.FoodOrderId == orderId);

            return boxMigration;
        }

        public void Create(BoxMigration boxMigration)
        {
            db.BoxMigration.Add(boxMigration);
        }

        public void Update(BoxMigration boxMigration)
        {
            db.Entry(boxMigration).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            BoxMigration boxMigration = db.BoxMigration.Find((int)id);
            if (boxMigration != null)
                db.BoxMigration.Remove(boxMigration);
        }

        public IEnumerable<BoxMigration> GetMigrationsByCustomer(int id)
        {
            IEnumerable<BoxMigration> boxMigrations = db.BoxMigration.Include(x => x.FoodOrder)
               .Where(x => x.FoodOrder.CustomerId == id);
            
            return boxMigrations;
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
    
