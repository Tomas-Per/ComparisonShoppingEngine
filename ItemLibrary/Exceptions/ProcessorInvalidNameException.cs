using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary.Exceptions
{
    public class ProcessorInvalidNameException : Exception
    {
        public ProcessorInvalidNameException(string message) : base(message)
        {
        }
    }
}
