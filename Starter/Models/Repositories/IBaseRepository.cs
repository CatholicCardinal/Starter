using System.Collections.Generic;
using System.Threading.Tasks;

namespace Starter.Models.Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<List<T>> GetAll();
        T Get(int id);
        void Save(T model);
        Task BulkSave(IList<T> model);
        void RemoveAll();
        void Remove(T model);
        void Remove(int id);
    }
}
