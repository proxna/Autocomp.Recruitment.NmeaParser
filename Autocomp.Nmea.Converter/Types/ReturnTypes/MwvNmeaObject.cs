using Autocomp.Nmea.Converter.Interfaces;

namespace Autocomp.Nmea.Converter.Types.ReturnTypes
{
    public class MwvNmeaObject : INmeaObject
    {
        public Talker Talker { get; set; }

        public Header Header { get; set; }

        public double WindAngle { get; set; }

        public WindReference Reference { get; set; }

        public double WindSpeed { get; set; }

        public WindSpeedUnits Units { get; set; }

        public DataValid Status { get; set; }
        
        public string ToLogInformation()
        {
            throw new System.NotImplementedException();
        }
    }
}
