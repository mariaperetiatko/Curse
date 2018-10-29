using System;
using System.Collections.Generic;

namespace EasyEat.Models
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Menu = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Streete { get; set; }
        public int HouseNamber { get; set; }
        public double Xcoordinate { get; set; }
        public double Ycoordinate { get; set; }
        public int IsDeleted { get; set; }

        public virtual ICollection<Menu> Menu { get; set; }
    }
}
