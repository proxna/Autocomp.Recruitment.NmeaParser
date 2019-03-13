using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Converter.Exceptions
{
    public class UnknownHeaderException : Exception
    {
        public UnknownHeaderException()
        {
        }

        public UnknownHeaderException(string message) : base(message)
        {
        }
    }
}
