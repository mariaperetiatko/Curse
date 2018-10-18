using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEat.Models
{
    public class MealTime
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
