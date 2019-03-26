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

        public IEnumerable<CartPart> GetWholeEntityList()
        {
            return db.CartPart.Include(x => x.Cart).Include(x => x.Menu);
        }

        public IEnumerable<CartPart> GetWholeEntityByCustomerList(Customer customer)
        {
            return db.CartPart.Include(x => x.Cart).Include(x => x.Menu)
                .Where(x => x.CartId == customer.Id);
        }

        public CartPart GetEntity(object id)
        {
            CartPartKey key = (CartPartKey)id;
            //return db.CartPart.Include(x => x.Cart).Include(x => x.Menu)
            //    .SingleOrDefault(x => new { x.MenuId, x.CartId } == id);
            return db.CartPart.Find(key.MenuId, key.CartId);
        }

        public CartPart GetWholeEntity(object id)
        {
            CartPartKey key = (CartPartKey)id;
            return db.CartPart.Include(x => x.Cart).Include(x => x.Menu)
                .SingleOrDefault(x => x.MenuId == key.MenuId & x.CartId == key.CartId);
        }

        public Customer GetCustomer(string tokenId)
        {
            Customer customer = db.Customer.Where(x => x.IdentityId == tokenId).FirstOrDefault();
            return customer;
        }

        public IEnumerable<CartPart> GetCartPartByCustomer(int id)
        {
            IEnumerable<CartPart> cartPart = db.CartPart
                .Where(x => x.CartId == id);

            return cartPart;
        }

        public int GetTemperature()
        {
            CartPart cp = db.CartPart.Last();
            return cp.DishTemperature;
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
            CartPartKey key = (CartPartKey)id;
            CartPart cartPart = db.CartPart.Find(key.MenuId, key.CartId);
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

