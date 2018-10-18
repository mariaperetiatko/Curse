using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEat.Models
{
    public class Ingredient
    {
        public int DishId { get; set; }
        public int ProductId { get; set; }
        public int ProductWeight { get; set; }

        public virtual Dish Dish { get; set; }
        public virtual Product Product { get; set; }
    }
}
