using RawRabbit.Instantiation;
using RawRabbit.Serialization;

namespace Asdf.Application.Processor.Enrichers.PlainText
{
    public static class PlainTextPlugin
    {
        public static IClientBuilder UsePlainText(this IClientBuilder builder)
        {
            builder.Register(
                p => { },
                di => di.AddSingleton<ISerializer, PlainTextSerializer>());
            return builder;
        }
    }
}