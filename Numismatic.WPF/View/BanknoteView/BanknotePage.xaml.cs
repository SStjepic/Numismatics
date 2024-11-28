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
        
        public ObservableCollection<BanknoteDataViewModel> CurrentBanknotes { get; set; }
        public BanknoteDisplayViewModel BanknoteDisplayViewModel { get; set; }
        public BanknoteCRUDViewModel BanknoteCRUDViewModel { get; set; }

        public int pageNumber;
        public int pageSize;
        public BanknotePage()
        {
            InitializeComponent();

            CurrentBanknotes = new ObservableCollection<BanknoteDataViewModel>();
            BanknoteDisplayViewModel = new BanknoteDisplayViewModel();
            BanknoteCRUDViewModel = new BanknoteCRUDViewModel();

            pageNumber = 0;
            pageSize = 10;

            this.GetBanknotes();
        }

        private void GetBanknotes()
        {
            CurrentBanknotes.Clear();
            foreach(BanknoteDataViewModel banknote in BanknoteDisplayViewModel.GetBanknotes(1, 10))
            {
                CurrentBanknotes.Add(banknote);
            }
        }
    }
}
