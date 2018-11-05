using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEat.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyEat.Repositories
{
    public class IngredientRepository : IRepository<Ingredient>
    {
        private EatContext db;

        public IngredientRepository()
        {
            this.db = new EatContext();
        }

        public IEnumerable<Ingredient> GetEntityList()
        {
            return db.Ingredient.Include(x => x.Dish).Include(x => x.Product);
        }

        public Ingredient GetEntity(object id)
        {
            return db.Ingredient.Include(x => x.Dish).Include(x => x.Product)
                .SingleOrDefault(x => new { x.DishId, x.ProductId } == id);
        }

        public void Create(Ingredient ingredient)
        {
            db.Ingredient.Add(ingredient);
        }

        public void Update(Ingredient ingredient)
        {
            db.Entry(ingredient).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            Ingredient ingredient = db.Ingredient.Find(id);
            if (ingredient != null)
                db.Ingredient.Remove(ingredient);
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
