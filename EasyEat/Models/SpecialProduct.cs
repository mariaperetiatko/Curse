using System;
using System.Collections.Generic;

namespace EasyEat.Models
{
    public partial class SpecialProduct
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Allowance { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
