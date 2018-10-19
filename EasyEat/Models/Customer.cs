using System;
using System.Collections.Generic;

namespace EasyEat.Models
{
    public partial class Customer
    {
        public Customer()
        {
            DeliveryAddress = new HashSet<DeliveryAddress>();
            FavouriteDish = new HashSet<FavouriteDish>();
            FoodOrder = new HashSet<FoodOrder>();
            SpecialProduct = new HashSet<SpecialProduct>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Phone { get; set; }
        public int? CaloricGoal { get; set; }
        public int? FoodStyleId { get; set; }
        public int? Balance { get; set; }
        public int IsDeleted { get; set; }

        public virtual FoodStyle FoodStyle { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ICollection<DeliveryAddress> DeliveryAddress { get; set; }
        public virtual ICollection<FavouriteDish> FavouriteDish { get; set; }
        public virtual ICollection<FoodOrder> FoodOrder { get; set; }
        public virtual ICollection<SpecialProduct> SpecialProduct { get; set; }
    }
}
