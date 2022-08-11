using System;
using System.Collections.Generic;

namespace Starter.Deserialization.Implementation
{
    public interface IDeserialization<T> : IDisposable
    {
        public List<T> Deserialization(string filePath);
    }
}
