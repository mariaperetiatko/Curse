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
            return db.Ingredient;
        }

        public Ingredient GetEntity(object id)
        {
            IngredientKey key = (IngredientKey)id;
            return db.Ingredient
                .Find(key.DishId, key.ProductId);

        }

        public void Create(Ingredient ingredient)
        {
            Ingredient prbableIngredient = db.Ingredient
                .Find(ingredient.DishId, ingredient.ProductId);
            if(prbableIngredient == null)
                db.Ingredient.Add(ingredient);
        }

        public void Update(Ingredient ingredient)
        {
            db.Entry(ingredient).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            IngredientKey key = (IngredientKey)id;
            Ingredient ingredient = db.Ingredient.Find(key.DishId, key.ProductId);
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
