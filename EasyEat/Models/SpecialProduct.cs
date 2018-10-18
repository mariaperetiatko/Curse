using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEat.Models
{
    public class SpecialProduct
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Allowance { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
