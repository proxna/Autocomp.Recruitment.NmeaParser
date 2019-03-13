using Autocomp.Nmea.Converter.Exceptions;
using Autocomp.Nmea.Converter.Interfaces;
using Autocomp.Nmea.Converter.Parsers;
using Autocomp.Nmea.Converter.Types;
using Autocomp.Nmea.Converter.Types.ReturnTypes;
using System;

namespace Autocomp.Nmea.Converter.Helpers
{
    public class MessageParserFactory : IMessageParserFactory
    {
        public IMessageParser GetParserByHeader(Header header)
        {
            switch (header)
            {
                case Header.GLL:
                    return new GllParser();
                case Header.MWV:
                    return new MwvParser();
                default:
                    throw new UnknownHeaderException("Header is undefined");
            }
        }

        public IMessageParser GetParserByType(Type type)
        {
            if (type == typeof(GllNmeaObject))
            {
                return new GllParser();
            }
            if (type == typeof(MwvNmeaObject))
            {
                return new MwvParser();
            }
            throw new UnknownHeaderException($"Cannot create parser for type:{type}");
        }
    }
}
