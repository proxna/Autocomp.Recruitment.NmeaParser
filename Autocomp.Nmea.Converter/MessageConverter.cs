using Autocomp.Nmea.Common;
using Autocomp.Nmea.Converter.Exceptions;
using Autocomp.Nmea.Converter.Interfaces;
using Autocomp.Nmea.Converter.Types;
using Autocomp.Nmea.Converter.Types.ReturnTypes;
using System;

namespace Autocomp.Nmea.Converter
{
    public class MessageConverter : IMessageConverter
    {
        private readonly IMessageParserFactory messageParserFactory;

        public MessageConverter(IMessageParserFactory messageParserFactory)
        {
            this.messageParserFactory = messageParserFactory;
        }

        public BaseNmeaObject ConvertMessage(NmeaMessage message)
        {
            string headerRawValue = message.Header.Substring(2, 3);
            if(!Enum.IsDefined(typeof(Header), headerRawValue))
            {
                throw new UnknownHeaderException($"Header {headerRawValue} is undefined");
            }
            var parser = messageParserFactory.GetParserByHeader((Header)Enum.Parse(typeof(Header), headerRawValue));
            var result = parser.Parse(message);
            return (BaseNmeaObject)result.Object;
        }

        public TResult ConvertMessageTo<TResult>(NmeaMessage message)
        {
            var parser = messageParserFactory.GetParserByType(typeof(TResult));
            var result = parser.Parse(message);
            return (TResult)result.Object;
        }
    }
}
