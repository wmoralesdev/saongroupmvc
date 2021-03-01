using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace saongroupmvc.Models.Exceptions
{
    public class EmptyOrBadApiCallException : Exception
    {
        public EmptyOrBadApiCallException(string message) : base(message) { }
    }
}