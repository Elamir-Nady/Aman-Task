using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intrerfaces
{
    public interface IGenericRepostory<T>
    {
        IEnumerable<T> Get(string[] includes = null);
        T GetByID(int id, string[] includes = null);
        int Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
