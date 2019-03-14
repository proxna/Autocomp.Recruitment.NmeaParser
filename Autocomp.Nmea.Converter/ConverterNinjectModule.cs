using Autocomp.Nmea.Converter.Helpers;
using Autocomp.Nmea.Converter.Interfaces;
using Ninject;
using Ninject.Modules;

namespace Autocomp.Nmea.Converter
{
    public class ConverterNinjectModule : NinjectModule
    {
        private static IKernel kernel;

        public static IKernel GetKernel()
        {
            return kernel ?? (kernel = new StandardKernel(new ConverterNinjectModule()));
        }

        public override void Load()
        {
            Bind<IMessageParserFactory>().To<MessageParserFactory>();
            Bind<IMessageConverter>().To<MessageConverter>();
        }
    }
}
