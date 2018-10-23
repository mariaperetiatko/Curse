using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyEat.Repositories
{
    interface IRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetEntityList();
        T GetEntity(object key);
        void Create(T item);
        void Update(T item);
        void Delete(object key);
        void Save();
    }
}
