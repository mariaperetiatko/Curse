using System;
using System.Collections.Generic;

namespace EasyEat.Models
{
    public partial class Dish
    {
        public Dish()
        {
            FavouriteDish = new HashSet<FavouriteDish>();
            Ingredient = new HashSet<Ingredient>();
            Menu = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public string DishName { get; set; }
        public string DishDescription { get; set; }

        public virtual ICollection<FavouriteDish> FavouriteDish { get; set; }
        public virtual ICollection<Ingredient> Ingredient { get; set; }
        public virtual ICollection<Menu> Menu { get; set; }
    }
}
