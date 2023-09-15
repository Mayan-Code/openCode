using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Meal : BaseCreateEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsFavorite { get; set; }
        public User Creator { get; set; }
        public long CreatorId { get; set; }
        public List<ProductMeal> Products { get; set; }

        public Meal(string name)
        {
            (Name) = (name);
        }
    }
}
