using Numismatic.WPF.View.BanknoteView;
using Numismatic.WPF.ViewModels;
using Numismatic.WPF.ViewModels.BanknoteViewModel;
using Numismatics.CORE.DTO;
using Numismatics.CORE.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Numismatic.WPF.ViewModel.BanknoteViewModel
{
    public class BanknoteDisplayViewModel : INotifyPropertyChanged
    {
        
        private readonly BanknoteService _banknoteService;

        private ObservableCollection<BanknoteDataViewModel> _currentBanknotes;

        public ObservableCollection<BanknoteDataViewModel> CurrentBanknotes 
        {
            get => this._currentBanknotes;
            set
            {
                this._currentBanknotes = value;
                OnPropertyChanged(nameof(CurrentBanknotes));
            }
        }

        private BanknoteDataViewModel _selectedBanknote;
        public BanknoteDataViewModel SelectedBanknote
        {
            get => this._selectedBanknote;
            set
            {
                this._selectedBanknote = value;
                OnPropertyChanged(nameof(SelectedBanknote));
            }
        }

        private int _pageNumber;
        public int PageNumber
        {
            get => this._pageNumber;
            set
            {
                this._pageNumber = value;
                OnPropertyChanged(nameof(PageNumber));
            }
        }
        private int _pageSize;
        public int PageSize
        {
            get => this._pageSize;
            set
            {
                this._pageSize = value;
                OnPropertyChanged(nameof(PageSize));
            }
        }

        public ICommand AddBanknoteCommand {  get; set; }
        public ICommand UpdateBanknoteCommand { get; set; }
        public ICommand DeleteBanknoteCommand { get; set; }

        public BanknoteDisplayViewModel()
        {
            _banknoteService = new BanknoteService();
            CurrentBanknotes = new ObservableCollection<BanknoteDataViewModel>();
            PageNumber = 0;
            PageSize = 10;

            AddBanknoteCommand = new RelayCommand(c => CreateBanknote());
            UpdateBanknoteCommand = new RelayCommand(c => UpdateBanknote());
            DeleteBanknoteCommand = new RelayCommand(c => DeleteBanknote());

            GetBanknotes(PageNumber, PageSize);
        }

        private void GetBanknotes(int pageNumber, int pageSize)
        {
            CurrentBanknotes.Clear();
            foreach (BanknoteDTO banknoteDTO in _banknoteService.GetByPage(pageNumber, pageSize))
            {
                CurrentBanknotes.Add(new BanknoteDataViewModel(banknoteDTO));
            }
            OnPropertyChanged(nameof(CurrentBanknotes));
        }

        private void CreateBanknote()
        {
            BanknoteDetailsPage banknoteDetailsPage = new BanknoteDetailsPage(null);
            bool? result = banknoteDetailsPage.ShowDialog();
            if(result == true)
            {
                GetBanknotes(PageNumber, PageSize);
            }
        }

        private void DeleteBanknote()
        {
            if (SelectedBanknote != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete selected banknote?", "Delete", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _banknoteService.Delete(SelectedBanknote.ToBanknoteDTO());
                    GetBanknotes(PageNumber, PageSize);
                }
            }
            else
            {
                MessageBox.Show("Please, select banknote you want to delete", "Delete");
            }
        }

        private void UpdateBanknote()
        {
            if (SelectedBanknote == null)
            {
                MessageBox.Show("Please, select banknote you want to update", "Update");
            }
            else
            {
                BanknoteDetailsPage banknoteDetailsPage = new BanknoteDetailsPage(SelectedBanknote);
                bool? result = banknoteDetailsPage.ShowDialog();
                if (result == true)
                {
                    GetBanknotes(PageNumber, PageSize);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
