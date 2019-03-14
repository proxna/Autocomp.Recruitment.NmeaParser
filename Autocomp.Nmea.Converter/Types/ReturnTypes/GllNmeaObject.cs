using Autocomp.Nmea.Converter.Interfaces;
using System;

namespace Autocomp.Nmea.Converter.Types.ReturnTypes
{
    public class GllNmeaObject : INmeaObject
    {
        public Talker Talker { get; set; }

        public Header Header { get; set; }

        public double Latitude { get; set; }

        public LatitudeDirection LatitudeDirection { get; set; }

        public double Longitude { get; set; }

        public LongitudeDirection LongitudeDirection { get; set; }

        public DateTime DateTime { get; set; }

        public DataValid Status { get; set; }

        public ModeIndicator ModeIndicator { get; set; }
        

        public string ToLogInformation()
        {
            throw new NotImplementedException();
        }
    }
}
