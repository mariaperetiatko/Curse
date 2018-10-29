using System;
using System.Collections.Generic;

namespace EasyEat.Models
{
    public partial class FoodStyle
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
