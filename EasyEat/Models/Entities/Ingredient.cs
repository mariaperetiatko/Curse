using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyEat.Models
{
    public partial class Ingredient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DishId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }
        public int ProductWeight { get; set; }

        public virtual Dish Dish { get; set; }
        public virtual Product Product { get; set; }
    }
}
