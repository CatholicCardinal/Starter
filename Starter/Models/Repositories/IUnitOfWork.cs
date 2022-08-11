using System;

namespace Starter.Models.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Record> Records { get; }

        void Save();
    }
}
