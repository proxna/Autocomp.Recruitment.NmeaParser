using Autocomp.Nmea.Common;
using Autocomp.Nmea.Converter.Exceptions;
using Autocomp.Nmea.Converter.Interfaces;
using Autocomp.Nmea.Converter.Types;
using System;

namespace Autocomp.Nmea.Converter.Parsers
{
    public abstract class BaseParser : IMessageParser
    {
        public abstract ParserResult Parse(NmeaMessage message);

        protected Talker GetTalker(string messageHeader)
        {
            string talkerRawValue = messageHeader.Substring(0, 2).ToUpper();
            if(!Enum.IsDefined(typeof(Talker), talkerRawValue))
            {
                throw new UnknownTalkerException($"The talker {talkerRawValue} is undefined");
            }
            return (Talker)Enum.Parse(typeof(Talker), talkerRawValue);
        }
    }
}
