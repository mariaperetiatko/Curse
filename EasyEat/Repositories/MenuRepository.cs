using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyEat.Repositories
{
    public class MenuRepository : IRepository<Menu>
    {
        private EatContext db;

        public MenuRepository()
        {
            this.db = new EatContext();
        }

        public IEnumerable<Menu> GetEntityList()
        {
            return db.Menu;
        }

        public Menu GetEntity(object id)
        {
            return db.Menu.Find(id);
        }

        public void Create(Menu menu)
        {
            db.Menu.Add(menu);
        }

        public void Update(Menu menu)
        {
            db.Entry(menu).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            Menu menu = db.Menu.Find(id);
            if (menu != null)
                db.Menu.Remove(menu);
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
