using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyEat.Repositories
{
    public class CartRepository : IRepository<Cart>
    {
        private EatContext db;

        public CartRepository()
        {
            this.db = new EatContext();
        }

        public IEnumerable<Cart> GetEntityList()
        {
            return db.Cart;
        }

        public Cart GetEntity(object id)
        {
            return db.Cart.Find(id);
        }

        public void Create(Cart cart)
        {
            db.Cart.Add(cart);
        }

        public void Update(Cart cart)
        {
            db.Entry(cart).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            Cart cart = db.Cart.Find(id);
            if (cart != null)
                db.Cart.Remove(cart);
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
