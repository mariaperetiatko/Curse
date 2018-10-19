using System;
using System.Collections.Generic;

namespace EasyEat.Models
{
    public partial class FavouriteDish
    {
        public int CustomerId { get; set; }
        public int DishId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Dish Dish { get; set; }
    }
}
