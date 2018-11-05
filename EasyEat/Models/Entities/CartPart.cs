using System;
using System.Collections.Generic;

namespace EasyEat.Models
{
    public partial class CartPart
    {
        public int MenuId { get; set; }
        public int CartId { get; set; }
        public int DishCount { get; set; }
        public int DishTemperature { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
