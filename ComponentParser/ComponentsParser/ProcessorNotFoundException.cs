using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebParser.ComponentsParser
{
    public class ProcessorNotFoundException : Exception
    { 
        public ProcessorNotFoundException(string message) : base(message)
        {
        }
    }
}
