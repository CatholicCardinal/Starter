using Starter.Deserialization.Implementation;
using System;

namespace Starter.Deserialization.Factory
{
    public class DeserializatorFactory : IDeserializatorFactory
    {
        public IDeserialization<T> Resolve<T>(string method) where T : new()
        {
            return method switch
            {
                "Csv" => new CsvDeserialization<T>(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
