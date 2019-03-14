using Autocomp.Nmea.Common;
using Autocomp.Nmea.Converter;
using Autocomp.Nmea.Converter.Exceptions;
using Autocomp.Nmea.Converter.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Autocomp.Nmea.Converter_UnitTests
{
    [TestClass]
    public class ConverterTests
    {
        private IMessageConverter messageConverter;

        [TestInitialize]
        public void TestInitialize()
        {
            messageConverter = ConverterNinjectModule.GetKernel().Get<IMessageConverter>();
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownHeaderException))]
        public void ParseMessageWithUnknownHeader()
        {
            var message = NmeaMessage.FromString("$GPABC,4916.45,N,12311.12,W,225444.12,A,A*");
            var result = messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownHeaderException))]
        public void ParseMessageToUnsupportedType()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,N,12311.12,W,225444.12,A,A*");
            var result = messageConverter.ConvertMessageTo<byte>(message);
        }
    }
}
