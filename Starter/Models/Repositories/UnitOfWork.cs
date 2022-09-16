using Starter.Models.Data;
using System.Threading.Tasks;

namespace Starter.Models.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext dbContext;

        private IBaseRepository<Record> RecordRep;

        public UnitOfWork()
        {
            dbContext = new ApplicationContext();
        }

        public IBaseRepository<Record> Records
        {
            get
            {

                if (this.RecordRep == null)
                {
                    this.RecordRep = new BaseRepository<Record>(dbContext);
                }
                return RecordRep;
            }
        }

        public async Task Save()
        {
            await dbContext.SaveChangesAsync();
        }

        private bool disposed = false;

        protected async virtual Task Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    await dbContext.DisposeAsync();
                }
            }
            this.disposed = true;
        }

        public async void Dispose()
        {
            await Dispose(true);
        }

    }
}
