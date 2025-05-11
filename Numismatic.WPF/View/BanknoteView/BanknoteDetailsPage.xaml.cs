using Microsoft.Win32;
using Numismatic.WPF.ViewModel.BanknoteViewModel;
using Numismatic.WPF.ViewModel.CoinViewModel;
using Numismatic.WPF.ViewModels.BanknoteViewModel;
using Numismatics.CORE.Domain.Enum;
using Numismatics.CORE.DTO;
using Numismatics.CORE.Services;
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
using System.Windows.Shapes;

namespace Numismatic.WPF.View.BanknoteView
{
    /// <summary>
    /// Interaction logic for BanknoteDetailsView.xaml
    /// </summary>
    public partial class BanknoteDetailsPage: Window
    {
        public BanknoteCrudViewModel BanknoteCrudViewModel { get; set; }
        public BanknoteDetailsPage(BanknoteDataViewModel banknote)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            BanknoteCrudViewModel = new BanknoteCrudViewModel(banknote);
            this.DataContext = this;

            SetComboBox();
        }

        private void SetComboBox()
        {
            foreach (Era era in Enum.GetValues(typeof(Era)))
            {
                EraComboBox.Items.Add(era);
            }
            if (BanknoteCrudViewModel.CurrentBanknote != null)
            {
                EraComboBox.SelectedItem = BanknoteCrudViewModel.CurrentBanknote.Era;
            }
            foreach (MoneyQuality banknoteQuality in Enum.GetValues(typeof(MoneyQuality)))
            {
                BanknoteQualityComboBox.Items.Add(banknoteQuality);
            }

        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
