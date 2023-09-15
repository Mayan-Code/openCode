using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Filters
{
    public class MealFilters : BaseEntityFilter
    {
        public DateTime? Day { get; set; }
        public bool OnlyFavorite { get; set; }

    }
}
