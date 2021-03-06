﻿using System;
using System.Collections.Generic;

namespace EasyEat.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartPart = new HashSet<CartPart>();
        }

        public int CustomerId { get; set; }
        public int TotalCaloricValue { get; set; }
        public int AddressId { get; set; }
        public int MealTimeId { get; set; }
        public DateTime DeliveryDate { get; set; }

        public virtual DeliveryAddress Address { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual MealTime MealTime { get; set; }
        public virtual ICollection<CartPart> CartPart { get; set; }
    }
}
