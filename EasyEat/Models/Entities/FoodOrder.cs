using System;
using System.Collections.Generic;

namespace EasyEat.Models
{
    public partial class FoodOrder
    {
        public FoodOrder()
        {
            BoxMigration = new HashSet<BoxMigration>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TotalCost { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int AddressId { get; set; }

        public virtual DeliveryAddress Address { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<BoxMigration> BoxMigration { get; set; }

    }
}
