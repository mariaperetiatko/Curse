using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEat.Models
{
    public class Dish
    {
        public Dish()
        {
            CartPart = new HashSet<CartPart>();
            FavouriteDish = new HashSet<FavouriteDish>();
            Ingredient = new HashSet<Ingredient>();
            Menu = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public string DishName { get; set; }
        public string DishDescription { get; set; }

        public virtual ICollection<CartPart> CartPart { get; set; }
        public virtual ICollection<FavouriteDish> FavouriteDish { get; set; }
        public virtual ICollection<Ingredient> Ingredient { get; set; }
        public virtual ICollection<Menu> Menu { get; set; }
    }
}
