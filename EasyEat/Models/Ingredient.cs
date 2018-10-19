using System;
using System.Collections.Generic;

namespace EasyEat.Models
{
    public partial class Ingredient
    {
        public int DishId { get; set; }
        public int ProductId { get; set; }
        public int ProductWeight { get; set; }

        public virtual Dish Dish { get; set; }
        public virtual Product Product { get; set; }
    }
}
