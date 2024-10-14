using Microsoft.Win32;
using Numismatics.CORE.Domain.Enum;
using Numismatics.CORE.DTO;
using Numismatics.CORE.Services;
using Numismatics.WPF.ViewModels;
using Numismatics.WPF.ViewModels.BanknoteViewModel;
using Numismatics.WPF.ViewModels.CoinViewModel;
using Numismatics.WPF.ViewModels.CountryViewModel;
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

namespace Numismatics.WPF.Views
{
    /// <summary>
    /// Interaction logic for BanknoteWindow.xaml
    /// </summary>
    public partial class BanknoteWindow : Window
    {
        public BanknoteViewModel BanknoteViewModel { get; set; }
        public BanknoteDataViewModel CurrentBanknote { get; set; }
        public CountryViewModel CountryViewModel { get; set; }
        public NationalCurrencyService NationalCurrencyService { get; set; }

        private bool _update;
        public ObservableCollection<CountryDTO> Countries { get; set; }
        public ObservableCollection<CurrencyDTO> Currencies{ get; set; }
        public string BanknoteCode {  get; set; }
        public BanknoteWindow(BanknoteDTO? banknoteDTO)
        {
            InitializeComponent();
            CurrentBanknote = new BanknoteDataViewModel(banknoteDTO);
            BanknoteViewModel = new BanknoteViewModel();
            CountryViewModel = new CountryViewModel();
            NationalCurrencyService = new NationalCurrencyService();
            _update = banknoteDTO != null? true: false;
            

            Countries = new ObservableCollection<CountryDTO>();
            Currencies = new ObservableCollection<CurrencyDTO>();
            DataContext = this;
            SetComboBox();
        }

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

            foreach (var country in CountryViewModel.GetAllCountries())
            {
                Countries.Add(country);
            }
            foreach (Era era in Enum.GetValues(typeof(Era)))
            {
                EraComboBox.Items.Add(era);
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
                if (_update)
                {
                    BanknoteViewModel.UpdateBanknote(CurrentBanknote.ToBanknoteDTO());
                }
                else
                {
                    BanknoteViewModel.CreateBanknote(CurrentBanknote.ToBanknoteDTO());
                }
                Close();
            }
        }

        private void DeleteBanknoteQuality(object sender, RoutedEventArgs e)
        {
            CurrentBanknote.DeleteBanknoteQuality();
        }
    }
}
