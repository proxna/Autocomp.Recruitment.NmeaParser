using Autocomp.Nmea.Converter.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomp.Nmea.Converter.Interfaces
{
    public interface IMessageParserFactory
    {
        IMessageParser GetParserByHeader(Header header);

        IMessageParser GetParserByType(Type type);
    }
}
