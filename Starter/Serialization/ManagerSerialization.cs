using Starter.Serialization.Factory;
using Starter.Serialization.Implementation;
using System.Threading.Tasks;

namespace Starter.Serialization
{
    public class ManagerSerialization<T>
    {
        public ISerialization<T> serialization;

        private string methodSerialization;
        public string MethodSerialization { get => methodSerialization; set => methodSerialization = value; }
        private readonly ISerializatorFactory _factory;
        public ManagerSerialization(string methodSerialization, ISerializatorFactory factory)
        {
            MethodSerialization = methodSerialization;
            _factory = factory;
            serialization = _factory.Resolve<T>(MethodSerialization);
        }
        public async Task Export(string filePath, object data)
        {
            await serialization.Serialization(filePath, data);
            serialization.Dispose();
        }
    }
}
