using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Serialization
{
    public interface ISerialization<T> : IDisposable
    {
        public void Serialization(string filePath, object data);
        public void Dispose();

    }
}
