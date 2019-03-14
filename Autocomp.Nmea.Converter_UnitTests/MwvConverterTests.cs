using Autocomp.Nmea.Common;
using Autocomp.Nmea.Converter;
using Autocomp.Nmea.Converter.Exceptions;
using Autocomp.Nmea.Converter.Interfaces;
using Autocomp.Nmea.Converter.Types;
using Autocomp.Nmea.Converter.Types.ReturnTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;

namespace Autocomp.Nmea.Converter_UnitTests
{
    [TestClass]
    public class MwvConverterTests
    {
        private IMessageConverter messageConverter;

        [TestInitialize]
        public void TestInitialize()
        {
            messageConverter = ConverterNinjectModule.GetKernel().Get<IMessageConverter>();
        }

        [TestMethod]
        public void ParseMwvMessageNoError()
        {
            var message = NmeaMessage.FromString("$WIMWV,007,R,4.1,N,A*");
            var result = (MwvNmeaObject)messageConverter.ConvertMessage(message);
            Assert.AreEqual(Header.MWV, result.Header);
            Assert.AreEqual(Talker.WI, result.Talker);
            Assert.AreEqual(7, result.WindAngle);
            Assert.AreEqual(WindReference.Relative, result.Reference);
            Assert.AreEqual(4.1, result.WindSpeed);
            Assert.AreEqual(WindSpeedUnits.Knots, result.Units);
            Assert.AreEqual(DataValid.Valid, result.Status);
        }

        [TestMethod]
        public void ParseMwvMessageUsingGenericMethod()
        {
            var message = NmeaMessage.FromString("$WIMWV,007,R,4.1,N,A*");
            var result = messageConverter.ConvertMessageTo<MwvNmeaObject>(message);
            Assert.AreEqual(Header.MWV, result.Header);
            Assert.AreEqual(Talker.WI, result.Talker);
            Assert.AreEqual(7, result.WindAngle);
            Assert.AreEqual(WindReference.Relative, result.Reference);
            Assert.AreEqual(4.1, result.WindSpeed);
            Assert.AreEqual(WindSpeedUnits.Knots, result.Units);
            Assert.AreEqual(DataValid.Valid, result.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongHeaderException))]
        public void ParseMwvMessageUsingGenericMethodWrongHeader()
        {
            var message = NmeaMessage.FromString("$WIGLL,007,R,4.1,N,A*");
            var result = messageConverter.ConvertMessageTo<MwvNmeaObject>(message);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownHeaderException))]
        public void ParseMwvMessageUsingGenericMethodUnknownHeader()
        {
            var message = NmeaMessage.FromString("$WIABC,007,R,4.1,N,A*");
            var result = messageConverter.ConvertMessageTo<MwvNmeaObject>(message);
        }

        [TestMethod]
        public void ParseMwvMessageNoErrorRefTheoretical()
        {
            var message = NmeaMessage.FromString("$WIMWV,007,T,4.1,N,A*");
            var result = (MwvNmeaObject)messageConverter.ConvertMessage(message);
            Assert.AreEqual(Header.MWV, result.Header);
            Assert.AreEqual(Talker.WI, result.Talker);
            Assert.AreEqual(7, result.WindAngle);
            Assert.AreEqual(WindReference.Theoretical, result.Reference);
            Assert.AreEqual(4.1, result.WindSpeed);
            Assert.AreEqual(WindSpeedUnits.Knots, result.Units);
            Assert.AreEqual(DataValid.Valid, result.Status);
        }

        [TestMethod]
        public void ParseMwvMessageNoErrorUnitKmPerHour()
        {
            var message = NmeaMessage.FromString("$WIMWV,007,R,4.1,K,A*");
            var result = (MwvNmeaObject)messageConverter.ConvertMessage(message);
            Assert.AreEqual(Header.MWV, result.Header);
            Assert.AreEqual(Talker.WI, result.Talker);
            Assert.AreEqual(7, result.WindAngle);
            Assert.AreEqual(WindReference.Relative, result.Reference);
            Assert.AreEqual(4.1, result.WindSpeed);
            Assert.AreEqual(WindSpeedUnits.KmPerHour, result.Units);
            Assert.AreEqual(DataValid.Valid, result.Status);
        }

        [TestMethod]
        public void ParseMwvMessageNoErrorUnitMeterPerSec()
        {
            var message = NmeaMessage.FromString("$WIMWV,007,R,4.1,M,A*");
            var result = (MwvNmeaObject)messageConverter.ConvertMessage(message);
            Assert.AreEqual(Header.MWV, result.Header);
            Assert.AreEqual(Talker.WI, result.Talker);
            Assert.AreEqual(7, result.WindAngle);
            Assert.AreEqual(WindReference.Relative, result.Reference);
            Assert.AreEqual(4.1, result.WindSpeed);
            Assert.AreEqual(WindSpeedUnits.MetersPerSecond, result.Units);
            Assert.AreEqual(DataValid.Valid, result.Status);
        }

        [TestMethod]
        public void ParseMwvMessageNoErrorUnitMilesPerSecond()
        {
            var message = NmeaMessage.FromString("$WIMWV,007,R,4.1,S,A*");
            var result = (MwvNmeaObject)messageConverter.ConvertMessage(message);
            Assert.AreEqual(Header.MWV, result.Header);
            Assert.AreEqual(Talker.WI, result.Talker);
            Assert.AreEqual(7, result.WindAngle);
            Assert.AreEqual(WindReference.Relative, result.Reference);
            Assert.AreEqual(4.1, result.WindSpeed);
            Assert.AreEqual(WindSpeedUnits.MilesPerHour, result.Units);
            Assert.AreEqual(DataValid.Valid, result.Status);
        }

        [TestMethod]
        public void ParseMwvMessageNoErrorNotValidData()
        {
            var message = NmeaMessage.FromString("$WIMWV,007,R,4.1,K,V*");
            var result = (MwvNmeaObject)messageConverter.ConvertMessage(message);
            Assert.AreEqual(Header.MWV, result.Header);
            Assert.AreEqual(Talker.WI, result.Talker);
            Assert.AreEqual(7, result.WindAngle);
            Assert.AreEqual(WindReference.Relative, result.Reference);
            Assert.AreEqual(4.1, result.WindSpeed);
            Assert.AreEqual(WindSpeedUnits.KmPerHour, result.Units);
            Assert.AreEqual(DataValid.Invalid, result.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownTalkerException))]
        public void ParseMwvMessageUnknownTalker()
        {
            var message = NmeaMessage.FromString("$ZZMWV,007,R,4.1,S,A*");
            var result = (MwvNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseMwvMessageWrongAngleFormat()
        {
            var message = NmeaMessage.FromString("$WIMWV,gerdbg,R,4.1,S,A*");
            var result = (MwvNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ParseMwvMessageAngleOutOfRange()
        {
            var message = NmeaMessage.FromString("$WIMWV,382,R,4.1,S,A*");
            var result = (MwvNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseMwvMessageWrongReference()
        {
            var message = NmeaMessage.FromString("$WIMWV,007,Z,4.1,S,A*");
            var result = (MwvNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseMwvMessageWrongUnits()
        {
            var message = NmeaMessage.FromString("$WIMWV,007,R,4.1,Z,A*");
            var result = (MwvNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseMwvMessageWrongDataValid()
        {
            var message = NmeaMessage.FromString("$WIMWV,007,R,4.1,S,Z*");
            var result = (MwvNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseMwvMessageWrongSpeedFormat()
        {
            var message = NmeaMessage.FromString("$WIMWV,007,R,gsergseg,S,A*");
            var result = (MwvNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ParseMwvMessageSpeedOutOfRange()
        {
            var message = NmeaMessage.FromString("$WIMWV,007,R,-4.7,S,A*");
            var result = (MwvNmeaObject)messageConverter.ConvertMessage(message);
        }
    }
}
