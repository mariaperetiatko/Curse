using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEat.Models
{
    public class FoodStyleProduct
    {
        public int FoodStyleId { get; set; }
        public int ProductId { get; set; }

        public virtual FoodStyle FoodStyle { get; set; }
        public virtual Product Product { get; set; }
    }
}
