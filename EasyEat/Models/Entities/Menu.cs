using System;
using System.Collections.Generic;

namespace EasyEat.Models
{
    public partial class Menu
    {
        public Menu()
        {
            CartPart = new HashSet<CartPart>();
        }
        public int Id { get; set; }
        public int DishId { get; set; }
        public int RestaurantId { get; set; }
        public int Cost { get; set; }

        public virtual Dish Dish { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<CartPart> CartPart { get; set; }

    }
}
