﻿using Microsoft.Win32;
using Numismatics.WPF.ViewModels.CoinViewModels;
using Numismatics.CORE.DTOs;
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
using Numismatics.CORE.Domains.Models;
using Numismatics.WPF.ViewModels.BanknoteViewModels;
using Numismatics.CORE.Domains.Enums;

namespace Numismatics.WPF.View.BanknoteView
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
