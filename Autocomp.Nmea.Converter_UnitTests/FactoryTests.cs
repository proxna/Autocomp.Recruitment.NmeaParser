using Autocomp.Nmea.Converter;
using Autocomp.Nmea.Converter.Exceptions;
using Autocomp.Nmea.Converter.Interfaces;
using Autocomp.Nmea.Converter.Parsers;
using Autocomp.Nmea.Converter.Types;
using Autocomp.Nmea.Converter.Types.ReturnTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Autocomp.Nmea.Converter_UnitTests
{
    [TestClass]
    public class FactoryTests
    {
        private IMessageParserFactory parserFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            parserFactory = ConverterNinjectModule.GetKernel().Get<IMessageParserFactory>();
        }

        [TestMethod]
        public void GetGllParserByHeader()
        {
            var result = parserFactory.GetParserByHeader(Header.GLL);
            Assert.AreEqual(typeof(GllParser), result.GetType());
        }

        [TestMethod]
        public void GetMwvParserByHeader()
        {
            var result = parserFactory.GetParserByHeader(Header.MWV);
            Assert.AreEqual(typeof(MwvParser), result.GetType());
        }

        [TestMethod]
        public void GetGllParserByType()
        {
            var result = parserFactory.GetParserByType(typeof(GllNmeaObject));
            Assert.AreEqual(typeof(GllParser), result.GetType());
        }

        [TestMethod]
        public void GetMwvParserByType()
        {
            var result = parserFactory.GetParserByType(typeof(MwvNmeaObject));
            Assert.AreEqual(typeof(MwvParser), result.GetType());
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownHeaderException))]
        public void GetParserByUnsupportedType()
        {
            var result = parserFactory.GetParserByType(typeof(byte));
        }
    }
}
