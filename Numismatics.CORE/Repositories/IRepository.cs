using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Repositories
{
    public interface IRepository<T>
    {
        public T? Get(int id);
        public T? Create(T entity);
        public T? Update(T entity);
        public T? Delete(int id);
        public List<T> GetAll();
    }
}
