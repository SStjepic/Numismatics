using Numismatics.WPF.ViewModels.Home.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numismatics.CORE.DTO;
using Numismatics.WPF.Views;

namespace Numismatics.WPF.ViewModels.Home
{
    public class HomeCountryViewModel : IHomeCRUDView
    {
        public void Add()
        {
            CountryView countryView = new CountryView(null);
            countryView.Show();
        }

        public void Delete<T>(T? entity)
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T? entity)
        {
            throw new NotImplementedException();
        }
    }
}
