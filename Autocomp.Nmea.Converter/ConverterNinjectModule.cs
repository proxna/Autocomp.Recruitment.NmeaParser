using Autocomp.Nmea.Converter.Helpers;
using Autocomp.Nmea.Converter.Interfaces;
using Ninject.Modules;

namespace Autocomp.Nmea.Converter
{
    public class ConverterNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMessageParserFactory>().To<MessageParserFactory>();
            Bind<IMessageConverter>().To<MessageConverter>();
        }
    }
}
