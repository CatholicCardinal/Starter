using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Deserialization.Implementation
{
    public interface IDeserialization<T> : IDisposable
    {
        public List<T> Deserialization(string filePath);
    }
}
