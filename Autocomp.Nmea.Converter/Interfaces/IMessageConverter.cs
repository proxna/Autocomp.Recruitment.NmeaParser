using Autocomp.Nmea.Common;
using Autocomp.Nmea.Converter.Types.ReturnTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Converter.Interfaces
{
    public interface IMessageConverter
    {
        BaseNmeaObject ConvertMessage(NmeaMessage message);

        TResult ConvertMessageTo<TResult>(NmeaMessage message);
    }
}
