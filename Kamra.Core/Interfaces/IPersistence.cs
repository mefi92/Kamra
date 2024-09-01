using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kamra.Core.Interfaces
{
    public interface IPersistence<T>
    {
        void Add(T entity);
        T GetByName(string name);
        IEnumerable<T> GetAll();
        void Remove(T entity);
        void Update(T entity);
    }
}
