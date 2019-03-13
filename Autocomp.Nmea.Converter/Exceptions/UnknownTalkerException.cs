using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Converter.Exceptions
{
    public class UnknownTalkerException : Exception
    {
        public UnknownTalkerException()
        {
        }

        public UnknownTalkerException(string message) : base(message)
        {
        }
    }
}
