using System;
using System.Threading.Tasks;

namespace Starter.Serialization.Implementation
{
    public interface ISerialization<T> : IDisposable
    {
        public Task Serialization(string filePath, object data);
    }
}
