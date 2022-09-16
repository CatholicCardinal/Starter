using Microsoft.EntityFrameworkCore;
using Starter.Models.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Starter.Models.Repositories
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private readonly ApplicationContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationContext applicationContext)
        {
            _dbContext = applicationContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async virtual Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual T Get(int id)
        {
            return _dbSet.SingleOrDefault(x => x.Id == id);
        }

        public virtual void Save(T model)
        {
            if (model.Id > 0)
            {
                _dbSet.Update(model);
            }
            else
            {
                _dbSet.Add(model);
            }
        }

        public async virtual Task BulkSave(IList<T> model)
        {
            await _dbSet.AddRangeAsync(model);
        }

        public virtual void RemoveAll()
        {
              _dbSet.RemoveRange(_dbSet);
        }

        public virtual void Remove(T model)
        {
            _dbContext.Remove(model);
        }

        public virtual void Remove(int id)
        {
            var model = Get(id);
            Remove(model);
        }
    }
}
