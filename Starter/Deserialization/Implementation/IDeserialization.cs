using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Starter.Deserialization.Implementation
{
    public interface IDeserialization<T> : IDisposable
    {
        public Task<List<T>> Deserialization(string filePath);
    }
}
