using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEat.Models
{
    public class FoodOrder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TotalCost { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int AddressId { get; set; }

        public virtual DeliveryAddress Address { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
