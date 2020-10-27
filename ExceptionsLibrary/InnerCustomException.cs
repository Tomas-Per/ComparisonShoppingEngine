using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionsLibrary
{
    public class InnerCustomException : Exception
    {
        public event EventHandler<string> InnerErrorEvent;
        public InnerCustomException(string message, Exception e) : base(message)
        {
            InnerErrorEvent?.Invoke(e, message);
        }
    }
}
