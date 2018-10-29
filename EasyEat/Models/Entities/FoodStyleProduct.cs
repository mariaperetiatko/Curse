using System;
using System.Collections.Generic;

namespace EasyEat.Models
{
    public partial class FoodStyleProduct
    {
        public int FoodStyleId { get; set; }
        public int ProductId { get; set; }

        public virtual FoodStyle FoodStyle { get; set; }
        public virtual Product Product { get; set; }
    }
}
