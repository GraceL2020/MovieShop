using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(String message)
            : base(message)
        {

        }
    }
}
