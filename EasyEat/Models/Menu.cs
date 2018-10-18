using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEat.Models
{
    public class Menu
    {
        public int DishId { get; set; }
        public int RestaurantId { get; set; }
        public int Cost { get; set; }

        public virtual Dish Dish { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
