using Starter.Models.Data;

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

        public void Save()
        {
            dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

    }
}
