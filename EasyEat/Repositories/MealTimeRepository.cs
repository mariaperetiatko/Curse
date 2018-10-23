using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyEat.Repositories
{
    public class MealTimeRepository : IRepository<MealTime>
    {
        private EatContext db;

        public MealTimeRepository()
        {
            this.db = new EatContext();
        }

        public IEnumerable<MealTime> GetEntityList()
        {
            return db.MealTime;
        }

        public MealTime GetEntity(object id)
        {
            return db.MealTime.Find(id);
        }

        public void Create(MealTime mealTime)
        {
            db.MealTime.Add(mealTime);
        }

        public void Update(MealTime mealTime)
        {
            db.Entry(mealTime).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            MealTime mealTime = db.MealTime.Find(id);
            if (mealTime != null)
                db.MealTime.Remove(mealTime);
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
