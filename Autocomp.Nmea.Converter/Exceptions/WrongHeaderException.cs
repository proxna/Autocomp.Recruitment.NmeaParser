using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Converter.Exceptions
{
    public class WrongHeaderException : Exception
    {
        public WrongHeaderException()
        {
        }

        public WrongHeaderException(string message) : base(message)
        {
        }
    }
}
