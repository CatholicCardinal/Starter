using Starter.Deserialization.Implementation;

namespace Starter.Deserialization.Factory
{
    public interface IDeserializatorFactory
    {
        public IDeserialization<T> Resolve<T>(string method) where T : new();
    }
}
