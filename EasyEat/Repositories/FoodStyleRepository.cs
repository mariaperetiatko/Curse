using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyEat.Repositories
{
    public class FoodStyleRepository : IRepository<FoodStyle>
    {
        private EatContext db;

        public FoodStyleRepository()
        {
            this.db = new EatContext();
        }

        public IEnumerable<FoodStyle> GetEntityList()
        {
            return db.FoodStyle;
        }

        public FoodStyle GetEntity(object id)
        {
            return db.FoodStyle.Find(id);
        }

        public void Create(FoodStyle foodStyle)
        {
            db.FoodStyle.Add(foodStyle);
        }

        public void Update(FoodStyle foodStyle)
        {
            db.Entry(foodStyle).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            FoodStyle foodStyle = db.FoodStyle.Find(id);
            if (foodStyle != null)
                db.FoodStyle.Remove(foodStyle);
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
