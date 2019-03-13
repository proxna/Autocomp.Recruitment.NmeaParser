namespace Autocomp.Nmea.Converter.Types.ReturnTypes
{
    public class MwvNmeaObject : BaseNmeaObject
    {
        public double WindAngle { get; set; }

        public WindReference Reference { get; set; }

        public double WindSpeed { get; set; }

        public WindSpeedUnits Units { get; set; }

        public DataValid Status { get; set; }
    }
}
