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
            CartParts = new HashSet<CartPart>();
            FavouriteDishes = new HashSet<FavouriteDish>();
            Ingredients = new HashSet<Ingredient>();
            Menus = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public string DishName { get; set; }
        public string DishDescription { get; set; }

        public virtual ICollection<CartPart> CartParts { get; set; }
        public virtual ICollection<FavouriteDish> FavouriteDishes { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
    }
}
