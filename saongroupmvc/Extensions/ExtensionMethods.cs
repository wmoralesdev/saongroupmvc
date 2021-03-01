using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace saongroupmvc.Extensions
{
    public static class ExtensionMethods
    {
        public static byte[] Encode(this string obj)
        {
            return Encoding.UTF8.GetBytes(obj);
        }
    }
}