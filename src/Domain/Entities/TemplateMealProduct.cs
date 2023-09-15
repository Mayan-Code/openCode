using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TemplateMealProduct : BaseCreateOnlyEntity
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }

        public long TemplateMealId { get; set; }
        public TemplateMeal TemplateMeal { get; set; }

        public decimal Quantity { get; set; }
        public string Description { get; set; }
    }
}
