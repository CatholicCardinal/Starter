using Starter.Deserialization.Factory;
using Starter.Deserialization.Implementation;
using System.Collections.Generic;

namespace Starter.Deserialization
{
    public class ManagerDeserialization<T> where T : new()
    {
        public IDeserialization<T> deserialization;

        private string methodDeserialization;
        public string MethodDeserialization { get => methodDeserialization; set => methodDeserialization = value; }
        private readonly IDeserializatorFactory _factory;
        public ManagerDeserialization(string methodDeserialization, IDeserializatorFactory factory)
        {
            MethodDeserialization = methodDeserialization;
            _factory = factory;
            deserialization = _factory.Resolve<T>(MethodDeserialization);
        }
        public List<T> Import(string filePath)
        {
            var result = deserialization.Deserialization(filePath);
            deserialization.Dispose();
            return result;
        }
    }
}
