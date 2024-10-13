using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModels.Home.Interfaces
{
    public interface IHomeViewModel
    {
        public object? Add();
        public object? Update(object entity);
        public object? Delete(object entity);
        public List<object> GetByPage(int pageNumber, int pageSize);
    }
}
