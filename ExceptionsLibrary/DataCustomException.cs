using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionsLibrary
{
    public class DataCustomException : Exception
    {
       public event EventHandler<string> DataErrorEvent;
       public DataCustomException(string message, object sender) : base(message)
        {
            DataErrorEvent?.Invoke(sender, message);
        }
    }
}
