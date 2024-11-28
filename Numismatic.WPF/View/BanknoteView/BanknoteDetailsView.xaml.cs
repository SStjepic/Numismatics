using Microsoft.Win32;
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
    public partial class BanknoteDetailsView : Window
    {
        public BanknoteDataViewModel CurrentBanknote {  get; set; }
        public BanknoteDetailsView()
        {
            InitializeComponent();
        }
        public NationalCurrencyService NationalCurrencyService { get; set; }

        private bool _update;
        public ObservableCollection<CountryDTO> Countries { get; set; }
        public ObservableCollection<CurrencyDTO> Currencies { get; set; }
        public string BanknoteCode { get; set; }

        private void CountryIsSelected(object sender, SelectionChangedEventArgs e)
        {
            if (CurrentBanknote.Country == null) return;
            var currencies = NationalCurrencyService.GetCurrencies(CurrentBanknote.Country.Id);
            Currencies.Clear();
            foreach (var currency in currencies)
            {
                Currencies.Add(currency);

            }
        }

        private void SetComboBox()
        {

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private string GetFilePath()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            fileDialog.FilterIndex = 1;
            if (fileDialog.ShowDialog() == true)
            {
                return fileDialog.FileName;
            }
            return "";
        }
        private void AddObversePicture(object sender, RoutedEventArgs e)
        {
            CurrentBanknote.ObversePicture = GetFilePath();
        }

        private void DeleteObversePicture(object sender, RoutedEventArgs e)
        {
            CurrentBanknote.ObversePicture = "";
        }

        private void AddReversePicture(object sender, RoutedEventArgs e)
        {
            CurrentBanknote.ReversePicture = GetFilePath();
        }

        private void DeleteReversePicture(object sender, RoutedEventArgs e)
        {
            CurrentBanknote.ReversePicture = "";
        }

        private void AddBanknoteQuality(object sender, RoutedEventArgs e)
        {
            CurrentBanknote.AddBanknoteQuality();
        }

        private void AddBanknote(object sender, RoutedEventArgs e)
        {
            if (CurrentBanknote.IsValid)
            {
                Close();
            }
        }

        private void DeleteBanknoteQuality(object sender, RoutedEventArgs e)
        {
            CurrentBanknote.DeleteBanknoteQuality();
        }
    }
}
