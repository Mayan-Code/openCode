using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TemplateMeal : BaseCreateEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public User Creator { get; set; }
        public long CreatorId { get; set; }
        public List<TemplateMealProduct> Products { get; set; }

        public TemplateMeal(string name)
        {
            (Name) = (name);
        }
    }
}
