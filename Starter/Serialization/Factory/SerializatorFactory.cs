using Starter.Serialization.Implementation;
using System;

namespace Starter.Serialization.Factory
{
    public class SerializatorFactory : ISerializatorFactory
    {
        public ISerialization<T> Resolve<T>(string method)
        {
            return method switch
            {
                "Excel" => new ExcelSerialization<T>(),
                "Xml" => new XmlSerialization<T>(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
