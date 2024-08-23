using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModels.Home.Interfaces
{
    public interface IHomeCRUDView
    {
        public void Add();
        public void Update(object entity);
        public void Delete(object entity);

    }
}
