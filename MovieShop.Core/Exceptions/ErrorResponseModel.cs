using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Core.Exceptions
{
    public class ErrorResponseModel
    {
        public string ErrorMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public string InnerException { get; set; }
    }
}
