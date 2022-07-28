using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Models.Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        List<T> GetAll();
        T Get(int id);
        void Save(T model);
        void BulkSave(IList<T> model);
        void RemoveAll();
        void Remove(T model);
        void Remove(int id);
    }
}
