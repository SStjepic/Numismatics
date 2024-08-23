using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModels.Home.Interfaces
{
    public interface IHomeCRUDView
    {
        public object Add();
        public object Update(object entity);
        public object Delete(object entity);

    }
}
