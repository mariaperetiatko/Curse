using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEat.Models
{
    public class FoodStyle
    {
        public FoodStyle()
        {
            Customer = new HashSet<Customer>();
            FoodStyleProduct = new HashSet<FoodStyleProduct>();
        }

        public int Id { get; set; }
        public string FoodStyleName { get; set; }
        public int CaloricValue { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<FoodStyleProduct> FoodStyleProduct { get; set; }
    }
}
