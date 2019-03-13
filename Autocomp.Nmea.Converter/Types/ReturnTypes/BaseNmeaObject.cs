namespace Autocomp.Nmea.Converter.Types.ReturnTypes
{
    public abstract class BaseNmeaObject
    {
        public Talker Talker { get; set; }

        public Header Header { get; set; }
    }
}
