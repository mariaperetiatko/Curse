using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyEat.Repositories
{
    public class CartPartRepository : IRepository<CartPart>
    {
        private EatContext db;

        public CartPartRepository()
        {
            this.db = new EatContext();
        }

        public IEnumerable<CartPart> GetEntityList()
        {
            return db.CartPart;
        }

        public CartPart GetEntity(object id)
        {
            return db.CartPart.Find(id);
        }

        public void Create(CartPart cartPart)
        {
            db.CartPart.Add(cartPart);
        }

        public void Update(CartPart cartPart)
        {
            db.Entry(cartPart).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            CartPart cartPart = db.CartPart.Find(id);
            if (cartPart != null)
                db.CartPart.Remove(cartPart);
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
}
