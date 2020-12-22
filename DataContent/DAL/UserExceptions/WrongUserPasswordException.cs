using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContent.DAL.UserExceptions
{
    public class WrongUserPasswordException : Exception
    {
        public WrongUserPasswordException(string message) : base(message) { }
    }
}
