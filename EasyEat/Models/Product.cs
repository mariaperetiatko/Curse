using System;
using System.Collections.Generic;

namespace EasyEat.Models
{
    public partial class Product
    {
        public Product()
        {
            FoodStyleProduct = new HashSet<FoodStyleProduct>();
            Ingredient = new HashSet<Ingredient>();
            SpecialProduct = new HashSet<SpecialProduct>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int CaloricValue { get; set; }

        public virtual ICollection<FoodStyleProduct> FoodStyleProduct { get; set; }
        public virtual ICollection<Ingredient> Ingredient { get; set; }
        public virtual ICollection<SpecialProduct> SpecialProduct { get; set; }
    }
}
