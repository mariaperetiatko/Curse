using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEat.Models
{
    public class Customer
    {
        public Customer()
        {
            DeliveryAddresses = new HashSet<DeliveryAddress>();
            FavouriteDishes = new HashSet<FavouriteDish>();
            FoodOrders = new HashSet<FoodOrder>();
            SpecialProducts = new HashSet<SpecialProduct>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Phone { get; set; }
        public int? CaloricGoal { get; set; }
        public int? FoodStyleId { get; set; }
        public int? Balance { get; set; }
        public bool IsDeleted { get; set; }

        public virtual FoodStyle FoodStyle { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ICollection<DeliveryAddress> DeliveryAddresses { get; set; }
        public virtual ICollection<FavouriteDish> FavouriteDishes { get; set; }
        public virtual ICollection<FoodOrder> FoodOrders { get; set; }
        public virtual ICollection<SpecialProduct> SpecialProducts { get; set; }
    }
}
