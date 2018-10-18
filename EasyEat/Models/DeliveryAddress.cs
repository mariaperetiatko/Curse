using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEat.Models
{
    public class DeliveryAddress
    {
        public DeliveryAddress()
        {
            Cart = new HashSet<Cart>();
            FoodOrder = new HashSet<FoodOrder>();
        }

        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Streete { get; set; }
        public int HouseNamber { get; set; }
        public int FlatNamber { get; set; }
        public double Xcoordinate { get; set; }
        public double Ycoordinate { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<FoodOrder> FoodOrder { get; set; }
    }
}
