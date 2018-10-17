using System;
using System.Text;
using RawRabbit.Serialization;

namespace Asdf.Application.Processor.Enrichers.PlainText
{
    public class PlainTextSerializer : ISerializer
    {
        public byte[] Serialize(object obj)
        {
            return Encoding.UTF8.GetBytes((string) obj);
        }

        public object Deserialize(Type type, byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public TType Deserialize<TType>(byte[] bytes)
        {
            return (TType) Deserialize(typeof(TType), bytes);
        }

        public string ContentType => string.Empty;
    }
}