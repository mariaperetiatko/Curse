using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEat.Models
{
    public class CartPart
    {
        public int DishId { get; set; }
        public int CartId { get; set; }
        public int DishCount { get; set; }
        public int DishTemperature { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Dish Dish { get; set; }
    }
}
