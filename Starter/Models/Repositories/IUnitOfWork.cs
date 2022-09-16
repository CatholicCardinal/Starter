using System;
using System.Threading.Tasks;

namespace Starter.Models.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Record> Records { get; }

        Task Save();
    }
}
