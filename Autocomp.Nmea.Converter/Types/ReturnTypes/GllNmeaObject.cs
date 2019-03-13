using System;

namespace Autocomp.Nmea.Converter.Types.ReturnTypes
{
    public class GllNmeaObject : BaseNmeaObject
    {
        public double Latitude { get; set; }

        public LatitudeDirection LatitudeDirection { get; set; }

        public double Longitude { get; set; }

        public LongitudeDirection LongitudeDirection { get; set; }

        public DateTime DateTime { get; set; }

        public DataValid Status { get; set; }

        public ModeIndicator ModeIndicator { get; set; }
    }
}
