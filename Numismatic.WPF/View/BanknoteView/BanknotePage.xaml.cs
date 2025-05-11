using Numismatic.WPF.ViewModel.BanknoteViewModel;
using Numismatic.WPF.ViewModels.BanknoteViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Numismatic.WPF.View
{
    /// <summary>
    /// Interaction logic for BanknotePage.xaml
    /// </summary>
    public partial class BanknotePage : Page
    {
        
        public BanknoteDisplayViewModel BanknoteDisplayViewModel { get; set; }
        public BanknotePage()
        {
            InitializeComponent();

            BanknoteDisplayViewModel = new BanknoteDisplayViewModel();
            DataContext = this;
        }

        private void ShowBanknotes(object sender, MouseButtonEventArgs e)
        {
            BanknoteDisplayViewModel.UpdateBanknoteCommand.Execute(this);
        }
    }
}
