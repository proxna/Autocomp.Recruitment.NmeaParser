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
    public class GllConverterTests
    {
        private IMessageConverter messageConverter;

        [TestInitialize]
        public void TestInitialize()
        {
            messageConverter = ConverterNinjectModule.GetKernel().Get<IMessageConverter>();
        }

        [TestMethod]
        public void ParseGllMessageNoError()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,N,12311.12,W,225444.12,A,A*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
            Assert.AreEqual(Header.GLL, result.Header);
            Assert.AreEqual(Talker.GP, result.Talker);
            Assert.AreEqual(4916.45, result.Latitude);
            Assert.AreEqual(LatitudeDirection.N, result.LatitudeDirection);
            Assert.AreEqual(12311.12, result.Longitude);
            Assert.AreEqual(LongitudeDirection.W, result.LongitudeDirection);
            Assert.AreEqual(DataValid.Valid, result.Status);
            Assert.AreEqual(ModeIndicator.Autonomous, result.ModeIndicator);
        }

        [TestMethod]
        public void ParseGllMessageUsingGenericMethod()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,N,12311.12,W,225444.12,A,A*");
            var result = messageConverter.ConvertMessageTo<GllNmeaObject>(message);
            Assert.AreEqual(Header.GLL, result.Header);
            Assert.AreEqual(Talker.GP, result.Talker);
            Assert.AreEqual(4916.45, result.Latitude);
            Assert.AreEqual(LatitudeDirection.N, result.LatitudeDirection);
            Assert.AreEqual(12311.12, result.Longitude);
            Assert.AreEqual(LongitudeDirection.W, result.LongitudeDirection);
            Assert.AreEqual(DataValid.Valid, result.Status);
            Assert.AreEqual(ModeIndicator.Autonomous, result.ModeIndicator);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongHeaderException))]
        public void ParseGllMessageUsingGenericMethodWrongHeader()
        {
            var message = NmeaMessage.FromString("$GPMWV,4916.45,N,12311.12,W,225444.12,A,A*");
            var result = messageConverter.ConvertMessageTo<GllNmeaObject>(message);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownHeaderException))]
        public void ParseGllMessageUsingGenericMethodUnknownHeader()
        {
            var message = NmeaMessage.FromString("$GPMWV,4916.45,N,12311.12,W,225444.12,A,A*");
            var result = messageConverter.ConvertMessageTo<GllNmeaObject>(message);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownTalkerException))]
        public void ParseGllMessageUnknownTalker()
        {
            var message = NmeaMessage.FromString("$GPMWV,4916.45,N,12311.12,W,225444.12,A,A*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownHeaderException))]
        public void ParseGllMessageUsingGenericMethodInvalidHeader()
        {
            var message = NmeaMessage.FromString("$GPAAP,4916.45,N,12311.12,W,225444.12,A,A*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        public void ParseGllMessageNoErrorSDirection()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,S,12311.12,W,225444.12,A,A*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
            Assert.AreEqual(Header.GLL, result.Header);
            Assert.AreEqual(Talker.GP, result.Talker);
            Assert.AreEqual(4916.45, result.Latitude);
            Assert.AreEqual(LatitudeDirection.S, result.LatitudeDirection);
            Assert.AreEqual(12311.12, result.Longitude);
            Assert.AreEqual(LongitudeDirection.W, result.LongitudeDirection);
            Assert.AreEqual(DataValid.Valid, result.Status);
            Assert.AreEqual(ModeIndicator.Autonomous, result.ModeIndicator);
        }

        [TestMethod]
        public void ParseGllMessageNoErrorEDirection()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,N,12311.12,E,225444.12,A,A*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
            Assert.AreEqual(Header.GLL, result.Header);
            Assert.AreEqual(Talker.GP, result.Talker);
            Assert.AreEqual(4916.45, result.Latitude);
            Assert.AreEqual(LatitudeDirection.N, result.LatitudeDirection);
            Assert.AreEqual(12311.12, result.Longitude);
            Assert.AreEqual(LongitudeDirection.E, result.LongitudeDirection);
            Assert.AreEqual(DataValid.Valid, result.Status);
            Assert.AreEqual(ModeIndicator.Autonomous, result.ModeIndicator);
        }

        [TestMethod]
        public void ParseGllMessageNoErrorInValidData()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,N,12311.12,W,225444.12,V,N*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
            Assert.AreEqual(Header.GLL, result.Header);
            Assert.AreEqual(Talker.GP, result.Talker);
            Assert.AreEqual(4916.45, result.Latitude);
            Assert.AreEqual(LatitudeDirection.N, result.LatitudeDirection);
            Assert.AreEqual(12311.12, result.Longitude);
            Assert.AreEqual(LongitudeDirection.W, result.LongitudeDirection);
            Assert.AreEqual(DataValid.Invalid, result.Status);
            Assert.AreEqual(ModeIndicator.DataNotValid, result.ModeIndicator);
        }

        [TestMethod]
        public void ParseGllMessageNoErrorDifferentialMode()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,N,12311.12,W,225444.12,A,D*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
            Assert.AreEqual(Header.GLL, result.Header);
            Assert.AreEqual(Talker.GP, result.Talker);
            Assert.AreEqual(4916.45, result.Latitude);
            Assert.AreEqual(LatitudeDirection.N, result.LatitudeDirection);
            Assert.AreEqual(12311.12, result.Longitude);
            Assert.AreEqual(LongitudeDirection.W, result.LongitudeDirection);
            Assert.AreEqual(DataValid.Valid, result.Status);
            Assert.AreEqual(ModeIndicator.Differential, result.ModeIndicator);
        }

        [TestMethod]
        public void ParseGllMessageNoErrorEstimatedMode()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,N,12311.12,W,225444.12,A,E*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
            Assert.AreEqual(Header.GLL, result.Header);
            Assert.AreEqual(Talker.GP, result.Talker);
            Assert.AreEqual(4916.45, result.Latitude);
            Assert.AreEqual(LatitudeDirection.N, result.LatitudeDirection);
            Assert.AreEqual(12311.12, result.Longitude);
            Assert.AreEqual(LongitudeDirection.W, result.LongitudeDirection);
            Assert.AreEqual(DataValid.Valid, result.Status);
            Assert.AreEqual(ModeIndicator.Estimated, result.ModeIndicator);
        }

        [TestMethod]
        public void ParseGllMessageNoErrorManualInputMode()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,N,12311.12,W,225444.12,A,M*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
            Assert.AreEqual(Header.GLL, result.Header);
            Assert.AreEqual(Talker.GP, result.Talker);
            Assert.AreEqual(4916.45, result.Latitude);
            Assert.AreEqual(LatitudeDirection.N, result.LatitudeDirection);
            Assert.AreEqual(12311.12, result.Longitude);
            Assert.AreEqual(LongitudeDirection.W, result.LongitudeDirection);
            Assert.AreEqual(DataValid.Valid, result.Status);
            Assert.AreEqual(ModeIndicator.ManualInput, result.ModeIndicator);
        }

        [TestMethod]
        public void ParseGllMessageNoErrorSimulatorMode()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,N,12311.12,W,225444.12,A,S*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
            Assert.AreEqual(Header.GLL, result.Header);
            Assert.AreEqual(Talker.GP, result.Talker);
            Assert.AreEqual(4916.45, result.Latitude);
            Assert.AreEqual(LatitudeDirection.N, result.LatitudeDirection);
            Assert.AreEqual(12311.12, result.Longitude);
            Assert.AreEqual(LongitudeDirection.W, result.LongitudeDirection);
            Assert.AreEqual(DataValid.Valid, result.Status);
            Assert.AreEqual(ModeIndicator.Simulator, result.ModeIndicator);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ParseGllMessageOutOfRangeLat()
        {
            var message = NmeaMessage.FromString("$GPGLL,7890.88,N,12311.12,W,225444.12,A,A*1D");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseGllMessageWrongLatFormat()
        {
            var message = NmeaMessage.FromString("$GPGLL,agaewsgfeg,N,12311.12,W,225444.12,A,A*1D");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseGllMessageWrongLatDirection()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,Z,12311.12,W,225444.12,A,A*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ParseGllMessageOutOfRangeLong()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,N,19479.91,W,225444.12,A,A*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseGllMessageWrongLongFormat()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,N,ergdhbgfwge,W,225444.12,A,A*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseGllMessageWrongLongDirection()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,N,12311.12,Z,225444.12,A,A*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseGllMessageWrongTimeFormat()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,N,12311.12,W,ed1213tfgwgw,A,A*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseGllMessageWrongTimeValue()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,N,12311.12,W,258744.12,A,A*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseGllMessageWrongDataValidValue()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,N,12311.12,W,225444.12,N,A*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ParseGllMessageWrongModeIndicatorValue()
        {
            var message = NmeaMessage.FromString("$GPGLL,4916.45,Z,12311.12,W,225444.12,A,Q*");
            var result = (GllNmeaObject)messageConverter.ConvertMessage(message);
        }
    }
}
