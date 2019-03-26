using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEat.Models
{
    public class BoxMigration
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        public DateTime Moment { get; set; }
        public int FoodOrderId { get; set; }
        public string Temperature { get; set; }

        public virtual FoodOrder FoodOrder { get; set; }
    }
}
