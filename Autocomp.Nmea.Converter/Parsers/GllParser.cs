using Autocomp.Nmea.Common;
using Autocomp.Nmea.Converter.Types;
using Autocomp.Nmea.Converter.Types.ReturnTypes;
using System;
using System.Globalization;

namespace Autocomp.Nmea.Converter.Parsers
{
    public class GllParser : BaseParser
    {
        public override ParserResult Parse(NmeaMessage message)
        {
            var nmeaObject = new GllNmeaObject
            {
                Talker = GetTalker(message.Header),
                Header = Header.GLL
            };

            GetHeaderValid(message.Header, nmeaObject.Header);

            if (!double.TryParse(message.Fields[0], out double latitude))
            {
                throw new FormatException($"Cannot parse value {nameof(nmeaObject.Latitude)}");
            }
            if (latitude < 0 || latitude > 9000 || latitude % 100 >= 60 || latitude - (int)latitude >= 0.6)
            {
                throw new ArgumentOutOfRangeException($"{nameof(nmeaObject.Latitude)} are out of valid range");
            }
            nmeaObject.Latitude = latitude;

            if (!Enum.IsDefined(typeof(LatitudeDirection), message.Fields[1]))
            {
                throw new FormatException($"Cannot parse value {nameof(nmeaObject.LatitudeDirection)}");
            }
            nmeaObject.LatitudeDirection = (LatitudeDirection)Enum.Parse(typeof(LatitudeDirection), message.Fields[1]);

            if (!double.TryParse(message.Fields[2], out double longitude))
            {
                throw new FormatException($"Cannot parse value {nameof(nmeaObject.Longitude)}");
            }
            if (longitude < 0 || longitude > 18000 || longitude % 100 >= 60 || longitude - (int)longitude >= 0.6)
            {
                throw new ArgumentOutOfRangeException($"{nameof(nmeaObject.Longitude)} are out of valid range");
            }
            nmeaObject.Longitude = longitude;

            if (!Enum.IsDefined(typeof(LongitudeDirection), message.Fields[3]))
            {
                throw new FormatException($"Cannot parse value {nameof(nmeaObject.LongitudeDirection)}");
            }
            nmeaObject.LongitudeDirection = (LongitudeDirection)Enum.Parse(typeof(LongitudeDirection), message.Fields[3]);

            if (!DateTime.TryParseExact(message.Fields[4], "HHmmss.FF", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime dateTime))
            {
                throw new FormatException($"Cannot parse value {nameof(nmeaObject.DateTime)}");
            }
            nmeaObject.DateTime = dateTime;

            if (!Enum.IsDefined(typeof(DataValid), message.Fields[5][0]))
            {
                throw new FormatException($"Cannot parse value {nameof(nmeaObject.Status)}");
            }
            nmeaObject.Status = (DataValid)message.Fields[5][0];

            if(!Enum.IsDefined(typeof(ModeIndicator), message.Fields[6][0]))
            {
                throw new FormatException($"Cannot parse value {nameof(nmeaObject.ModeIndicator)}");
            }
            nmeaObject.ModeIndicator = (ModeIndicator)message.Fields[6][0];

            return new ParserResult
            {
                Type = typeof(GllNmeaObject),
                Object = nmeaObject
            };
        }
    }
}
