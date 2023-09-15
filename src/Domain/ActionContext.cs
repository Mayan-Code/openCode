using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ActionContext
    {
        public string Username { get; set; }
        public string ActionSource { get; set; }
        public string IpAddress { get; set; }
    }
}
