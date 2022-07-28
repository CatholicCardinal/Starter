using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Models.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Record> Records { get; }

        void Save();
    }
}
