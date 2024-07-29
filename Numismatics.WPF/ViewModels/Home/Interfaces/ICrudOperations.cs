using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModels.Home.Interfaces
{
    public interface ICrudOperations<T>
    {
        public T? Add(T?  entity);
        public T? Delete(T? entity);
        public T? Update(T? entity);
        public List<T> GetAll();
        public T? Get(int id);
    }
}
