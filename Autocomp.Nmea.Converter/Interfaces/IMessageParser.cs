using Autocomp.Nmea.Common;
using Autocomp.Nmea.Converter.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Converter.Interfaces
{
    public interface IMessageParser
    {
        INmeaObject Parse(NmeaMessage message);
    }
}
