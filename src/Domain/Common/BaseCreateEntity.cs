using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class BaseCreateEntity
    {
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public DateTime? ChangeDateTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }

    public abstract class BaseCreateOnlyEntity
    {
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public DateTime? ChangeDateTime { get; set; } = DateTime.Now;
    }
}
