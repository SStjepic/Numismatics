using Numismatics.WPF.ViewModels.CountryViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Numismatics.WPF.View.CountryView
{
    /// <summary>
    /// Interaction logic for CountryPage.xaml
    /// </summary>
    public partial class CountryPage : Page
    {
        public CountryDisplayViewModel CountryDisplayViewModel { get; set; }
        public CountryPage()
        {
            InitializeComponent();
            CountryDisplayViewModel = new CountryDisplayViewModel();
            DataContext = this;
        }

        private void ShowCountry(object sender, MouseButtonEventArgs e)
        {
            CountryDisplayViewModel.UpdateCountryCommand.Execute(e);
        }
    }
}
