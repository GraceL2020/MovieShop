using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(String message)
            :base(message)
        {

        }
    }
}
