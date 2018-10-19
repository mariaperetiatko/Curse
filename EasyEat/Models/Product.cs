using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEat.Models
{
    public class Product
    {
        public Product()
        {
            FoodStyleProducts = new HashSet<FoodStyleProduct>();
            Ingredients = new HashSet<Ingredient>();
            SpecialProducts = new HashSet<SpecialProduct>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int CaloricValue { get; set; }

        public virtual ICollection<FoodStyleProduct> FoodStyleProducts { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<SpecialProduct> SpecialProducts { get; set; }
    }
}
