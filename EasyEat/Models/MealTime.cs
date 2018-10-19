using System;
using System.Collections.Generic;

namespace EasyEat.Models
{
    public partial class MealTime
    {
        public MealTime()
        {
            Cart = new HashSet<Cart>();
        }

        public int Id { get; set; }
        public int AllowedCaloricValue { get; set; }
        public TimeSpan MealTimestamp { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
    }
}
