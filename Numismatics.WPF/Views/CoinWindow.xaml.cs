using Microsoft.Win32;
using Numismatics.CORE.Domain.Enum;
using Numismatics.CORE.Domain.Models;
using Numismatics.CORE.DTO;
using Numismatics.CORE.Services;
using Numismatics.WPF.ViewModels;
using Numismatics.WPF.ViewModels.CoinViewModel;
using Numismatics.WPF.ViewModels.CountryViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for CoinWindow.xaml
    /// </summary>
    public partial class CoinWindow : Window
    {

        public CoinViewModel CoinViewModel { get; set; }
        public CoinDataViewModel CurrentCoin { get; set; }
        public CountryViewModel CountryViewModel { get; set; }
        public NationalCurrencyService NationalCurrencyService { get; set; }

        private bool _update;
        public ObservableCollection<CountryDTO> Countries { get; set; }
        public ObservableCollection<CurrencyDTO> Currencies
        { get; set; }
        public ObservableCollection<string> CurrencyHundredthPart { get; set; }
        public CoinWindow(CoinDTO? coinDTO)
        {
            InitializeComponent();
            CurrentCoin = new CoinDataViewModel(coinDTO);
            CoinViewModel = new CoinViewModel();
            CountryViewModel = new CountryViewModel();
            NationalCurrencyService = new NationalCurrencyService();
            _update = coinDTO == null ? false : true;
            DataContext = this;
            
            Countries = new ObservableCollection<CountryDTO>();
            Currencies = new ObservableCollection<CurrencyDTO>();
            CurrencyHundredthPart = new ObservableCollection<string>();




            SetComboBox();
        }

        private void CountryIsSelected(object sender, SelectionChangedEventArgs e)
        {
            if (CurrentCoin.Country == null) return;
            var currencies = NationalCurrencyService.GetCurrencies(CurrentCoin.Country.Id);
            Currencies.Clear();
            foreach (var currency in currencies) 
            {
                Currencies.Add(currency);

            }
        }

        private void CurrencyIsSelected(object sender, SelectionChangedEventArgs e)
        {
            if (CurrentCoin.Currency == null) return;
            CurrencyHundredthPart.Clear();
            CurrencyHundredthPart.Add(CurrentCoin.Currency.HunderthPartName);
            CurrencyHundredthPart.Add("");
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
            foreach(MoneyQuality banknoteQuality in Enum.GetValues(typeof(MoneyQuality)))
            {
                CoinQualityComboBox.Items.Add(banknoteQuality);
            }

        }

        private void AddCoin(object sender, RoutedEventArgs e)
        {
            if(CurrentCoin.IsValid)
            {
                if (_update)
                {
                    CoinViewModel.UpdateCoin(CurrentCoin.ToCoinDTO());
                }
                else
                {
                    CoinViewModel.CreateCoin(CurrentCoin.ToCoinDTO());
                }
                Close();
            }
        }

        private void AddCoinQuality(object sender, RoutedEventArgs e)
        {
            var selectedQuality = CoinQualityComboBox.SelectedItem.ToString();
            MoneyQuality coinQuality = (MoneyQuality)Enum.Parse(typeof(MoneyQuality), selectedQuality);
            QualityKeyValuePair<MoneyQuality, int> pair = CurrentCoin.Coins.FirstOrDefault(p => p.Key.Equals(coinQuality));
            if (pair == null)
            {
                CurrentCoin.Coins.Add(new QualityKeyValuePair<MoneyQuality, int>(coinQuality, 1));
            }
            else
            {
                var number = pair.Value;
                CurrentCoin.Coins.Remove(pair);
                CurrentCoin.Coins.Add(new QualityKeyValuePair<MoneyQuality, int>(coinQuality, number + 1));
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
            CurrentCoin.ObversePicture = GetFilePath();
        }

        private void DeleteObversePicture(object sender, RoutedEventArgs e)
        {
            CurrentCoin.ObversePicture = "";
        }

        private void AddReversePicture(object sender, RoutedEventArgs e)
        {
            CurrentCoin.ReversePicture = GetFilePath();
        }

        private void DeleteReversePicture(object sender, RoutedEventArgs e)
        {
            CurrentCoin.ReversePicture = "";
        }
    }
}
