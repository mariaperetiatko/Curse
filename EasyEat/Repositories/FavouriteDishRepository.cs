﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyEat.Repositories
{
    public class FavouriteDishRepository : IRepository<FavouriteDish>
    {
        private EatContext db;

        public FavouriteDishRepository()
        {
            this.db = new EatContext();
        }

        public IEnumerable<FavouriteDish> GetEntityList()
        {
            return db.FavouriteDish.Include(x => x.Customer).Include(x => x.Dish);
        }

        public FavouriteDish GetEntity(object id)
        {
            return db.FavouriteDish.Include(x => x.Customer).Include(x => x.Dish)
                .SingleOrDefault(x => new { x.CustomerId, x.DishId } == id);
        }

        public void Create(FavouriteDish favouriteDish)
        {
            db.FavouriteDish.Add(favouriteDish);
        }

        public void Update(FavouriteDish favouriteDish)
        {
            db.Entry(favouriteDish).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            FavouriteDish favouriteDish = db.FavouriteDish.Find(id);
            if (favouriteDish != null)
                db.FavouriteDish.Remove(favouriteDish);
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
