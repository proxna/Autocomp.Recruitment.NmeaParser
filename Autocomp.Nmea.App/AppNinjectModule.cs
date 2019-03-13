using Autocomp.Nmea.App.ViewModel;
using Autocomp.Nmea.Converter;
using Ninject;
using Ninject.Modules;

namespace Autocomp.Nmea.App
{
    public class AppNinjectModule : NinjectModule
    {
        private static IKernel kernel;

        public static IKernel GetKernel()
        {
            return kernel ?? (kernel = new StandardKernel(new AppNinjectModule(), new ConverterNinjectModule()));
        }

        public override void Load()
        {
            Bind<MainViewModel>().ToSelf();
        }
    }
}
