using Autocomp.Nmea.Converter.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Converter.Interfaces
{
    public interface INmeaObject
    {
        Talker Talker { get; set; }

        Header Header { get; set; }

        string ToLogInformation();
    }
}
