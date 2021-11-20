using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtChangeLog.DataBase.Repositories.Interfaces
{
    public interface ICRUD<T> where T : new()
    {
        T GetEntity(Guid guid);
        void AddEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(Guid guid);
    }
}
