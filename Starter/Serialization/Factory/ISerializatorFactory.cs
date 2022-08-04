using Starter.Serialization.Implementation;

namespace Starter.Serialization.Factory
{
    public interface ISerializatorFactory
    {
        public ISerialization<T> Resolve<T>(string method);
    }
}
