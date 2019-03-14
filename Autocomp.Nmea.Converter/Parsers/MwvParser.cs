using System;
using System.Globalization;
using Autocomp.Nmea.Common;
using Autocomp.Nmea.Converter.Interfaces;
using Autocomp.Nmea.Converter.Types;
using Autocomp.Nmea.Converter.Types.ReturnTypes;

namespace Autocomp.Nmea.Converter.Parsers
{
    public class MwvParser : BaseParser
    {
        public override INmeaObject Parse(NmeaMessage message)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
            var nmeaObject = new MwvNmeaObject
            {
                Talker = GetTalker(message.Header),
                Header = Header.MWV
            };

            GetHeaderValid(message.Header, nmeaObject.Header);

            if (!double.TryParse(message.Fields[0], out double angle))
            {
                throw new FormatException($"Cannot parse value {nameof(nmeaObject.WindAngle)}");
            }
            if (angle < 0 || angle >= 360)
            {
                throw new ArgumentOutOfRangeException($"Value {nameof(nmeaObject.WindAngle)} is out of range");
            }
            nmeaObject.WindAngle = angle;
            
            if(!Enum.IsDefined(typeof(WindReference), (int)message.Fields[1][0]))
            {
                throw new FormatException($"Cannot parse value {nameof(nmeaObject.Reference)}");
            }
            nmeaObject.Reference = (WindReference)message.Fields[1][0];

            if(!double.TryParse(message.Fields[2], out double speed))
            {
                throw new FormatException($"Cannot parse value {nameof(nmeaObject.WindSpeed)}");
            }
            if(speed < 0)
            {
                throw new ArgumentOutOfRangeException($"Value {nameof(nmeaObject.WindSpeed)} is out of range");
            }
            nmeaObject.WindSpeed = speed;

            if(!Enum.IsDefined(typeof(WindSpeedUnits), (int)message.Fields[3][0]))
            {
                throw new FormatException($"Cannot parse value {nameof(nmeaObject.Units)}");
            }
            nmeaObject.Units = (WindSpeedUnits)message.Fields[3][0];

            if(!Enum.IsDefined(typeof(DataValid), (int)message.Fields[4][0]))
            {
                throw new FormatException($"Cannot parse value {nameof(nmeaObject.Status)}");
            }
            nmeaObject.Status = (DataValid)message.Fields[4][0];

            return nmeaObject;
        }
    }
}
