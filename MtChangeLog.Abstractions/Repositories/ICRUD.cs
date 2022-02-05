using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.Abstractions.Repositories
{
    public interface ICRUD<T> where T : class, new()
    {
        T GetEntity(Guid guid);
        void AddEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(Guid guid);
    }
}
