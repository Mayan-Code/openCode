using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GlobalErrorHandlong
{
    public class NotYourObject : Exception
    {
        public override string Message { get;}
        
        public NotYourObject(string details = "")
        {
            (Message) = (details);
        }
    }
}
