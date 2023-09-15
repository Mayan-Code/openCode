using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductMeal : BaseCreateOnlyEntity
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }

        public long MealId { get; set; }
        public Meal Meal { get; set; }

        public decimal Quantity { get; set; }
        public string Description { get; set; }
    }
}
