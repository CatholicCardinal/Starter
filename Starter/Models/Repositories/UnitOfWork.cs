using Starter.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            
        }

    }
}
