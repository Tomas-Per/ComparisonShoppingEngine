using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionsLibrary
{
    class DataException : Exception
    {
       public event EventHandler<string> DataError;
       public DataException(string message, object sender) : base(message)
        {
            DataError?.Invoke(sender, message);
        }
    }
}
