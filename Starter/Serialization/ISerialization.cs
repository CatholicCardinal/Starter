using System;

namespace Starter.Serialization
{
    public interface ISerialization<T> : IDisposable
    {
        public void Serialization(string filePath, object data);
        public void Dispose();

    }
}
