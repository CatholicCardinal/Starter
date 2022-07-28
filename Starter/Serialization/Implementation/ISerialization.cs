using System;

namespace Starter.Serialization.Implementation
{
    public interface ISerialization<T> : IDisposable
    {
        public void Serialization(string filePath, object data);
    }
}
