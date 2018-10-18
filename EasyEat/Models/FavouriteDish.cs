using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEat.Models
{
    public class FavouriteDish
    {
        public int CustomerId { get; set; }
        public int DishId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Dish Dish { get; set; }
    }
}
