using Microsoft.Extensions.DependencyInjection;
using Numismatics.CORE.Domains.Enums;
using Numismatics.CORE.Services.Export;
using Numismatics.CORE.Services.Interface;
using Numismatics.WPF.Utils;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Input;

namespace Numismatics.WPF.ViewModels.ExportDataViewModel
{
    public class ExportDataViewModel: INotifyPropertyChanged
    {
        private readonly ExportDataServiceFactory _factory;
        private IExportDataService _exportDataService;

        public ICommand ExportDataCommand { get; set; }
        public ICommand ChooseExportPathCommand { get; set; }

        private bool _exportBanknotesData;
        public bool ExportBanknotesData
        {
            get => _exportBanknotesData;
            set
            {
                _exportBanknotesData = value;
                OnPropertyChanged(nameof(ExportBanknotesData));
                if (!value)
                {
                    ExportAll = false;
                }
            }
        }

        private bool _exportCoinsData;
        public bool ExportCoinsData
        {
            get => _exportCoinsData;
            set
            {
                _exportCoinsData = value;
                OnPropertyChanged(nameof(ExportCoinsData));
                if (!value)
                {
                    ExportAll = false;
                }
            }
        }

        private bool _exportCountriesData;
        public bool ExportCountriesData
        {
            get => _exportCountriesData;
            set
            {
                _exportCountriesData = value;
                OnPropertyChanged(nameof(ExportCountriesData));
                if (!value)
                {
                    ExportAll = false;
                }
            }
        }

        private bool _exportCurrenciesData;
        public bool ExportCurrenciesData
        {
            get => _exportCurrenciesData;
            set
            {
                _exportCurrenciesData = value;
                OnPropertyChanged(nameof(ExportCurrenciesData));
                if (!value)
                {
                    ExportAll = false;
                }
            }
        }

        private bool _exportOwnedBanknotesData;
        public bool ExportOwnedBanknotesData
        {
            get => _exportOwnedBanknotesData;
            set
            {
                _exportOwnedBanknotesData = value;
                OnPropertyChanged(nameof(ExportOwnedBanknotesData));
                if (!value)
                {
                    ExportAll = false;
                }
            }
        }

        private bool _exportOwnedCoinsData;
        public bool ExportOwnedCoinsData
        {
            get => _exportOwnedCoinsData;
            set
            {
                _exportOwnedCoinsData = value;
                OnPropertyChanged(nameof(ExportOwnedCoinsData));
                if (!value)
                {
                    ExportAll = false;
                }
            }
        }

        private bool _exportAll;
        public bool ExportAll
        {
            get => _exportAll;
            set
            {
                if (value) 
                {
                    ExportBanknotesData = value;
                    ExportCoinsData = value;
                    ExportCountriesData = value;
                    ExportCurrenciesData = value;
                    ExportOwnedBanknotesData = value;
                    ExportOwnedCoinsData = value;
                }

                _exportAll = value;

                OnPropertyChanged(nameof(ExportAll));
            }
        }

        private ExportFormat _selectedExportFormat = ExportFormat.Json;
        public ExportFormat SelectedExportFormat
        {
            get => _selectedExportFormat;
            set
            {
                _selectedExportFormat = value;
                OnPropertyChanged(nameof(SelectedExportFormat));
            }
        }
        private string _exportPath;
        public string ExportPath
        {
            get => _exportPath;
            set
            {
                _exportPath = value;
                OnPropertyChanged(nameof(ExportPath));
            }
        }

        public ExportDataViewModel()
        {
            _factory = App.AppHost.Services.GetRequiredService<ExportDataServiceFactory>();
            ExportDataCommand = new RelayCommand(c => ExportData());
            ChooseExportPathCommand = new RelayCommand(c => ChooseExportPath());
        }

        private bool ExportData()
        {
            try
            {
                _exportDataService = _factory.GetService(SelectedExportFormat);
                _exportDataService.ExportPath = this.ExportPath;
                if (ExportBanknotesData)
                {
                    _exportDataService.ExportBanknotesData();
                }
                if (ExportCoinsData)
                {
                    _exportDataService.ExportCoinsData();
                }
                if (ExportCountriesData)
                {
                    _exportDataService.ExportCountriesData();
                }
                if (ExportCurrenciesData)
                {
                    _exportDataService.ExportCurrenciesData();
                }
                if (ExportOwnedBanknotesData)
                {
                    _exportDataService.ExportOwnedBanknotesData();
                }
                if (ExportOwnedCoinsData)
                {
                    _exportDataService.ExportOwnedCoinsData();
                }
                MessageBox.Show("Data has been successfully exported!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"An error occurred while exporting data:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            return true;
        }

        private void ChooseExportPath()
        {
            var dialog = new FolderBrowserDialog
            {
                Description = "Choose export folder",
                ShowNewFolderButton = true
            };

            var result = dialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                ExportPath = dialog.SelectedPath;
                OnPropertyChanged(nameof(ExportPath));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
