using Numismatics.WPF.View.BanknoteView;
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
using Numismatics.WPF.Util;
using System.Security.Policy;

namespace Numismatics.WPF.ViewModel.BanknoteViewModel
{
    public class BanknoteDisplayViewModel : DisplayViewMode
    {
        
        private readonly BanknoteService _banknoteService;

        private ObservableCollection<BanknoteDataViewModel> _currentBanknotes;

        public ObservableCollection<BanknoteDataViewModel> CurrentBanknotes 
        {
            get => _currentBanknotes;
            set
            {
                _currentBanknotes = value;
                OnPropertyChanged(nameof(CurrentBanknotes));
            }
        }

        private BanknoteDataViewModel _selectedBanknote;
        public BanknoteDataViewModel SelectedBanknote
        {
            get => _selectedBanknote;
            set
            {
                _selectedBanknote = value;
                OnPropertyChanged(nameof(SelectedBanknote));
            }
        }

        public ICommand AddBanknoteCommand {  get; set; }
        public ICommand UpdateBanknoteCommand { get; set; }
        public ICommand DeleteBanknoteCommand { get; set; }

        public BanknoteDisplayViewModel()
        {
            _banknoteService = new BanknoteService();
            CurrentBanknotes = new ObservableCollection<BanknoteDataViewModel>();
            PageNumber = 1;
            PageSize = 10;
            TotalPages = _banknoteService.GetTotalPageNumber(PageSize);

            AddBanknoteCommand = new RelayCommand(c => CreateBanknote());
            UpdateBanknoteCommand = new RelayCommand(c => UpdateBanknote());
            DeleteBanknoteCommand = new RelayCommand(c => DeleteBanknote());
            GetNextPageCommand = new RelayCommand(c => GetNextPage());
            GetPreviousPageCommand = new RelayCommand(c => GetPreviousPage());

            GetBanknotes(PageNumber-1, PageSize);
        }

        

        public override void GetNextPage()
        {
            if (PageNumber+1 <= TotalPages) 
            {
                PageNumber++;
                GetBanknotes(PageNumber, TotalPages);
            }
        }

        public override void GetPreviousPage()
        {
            if(PageNumber - 1 > 0)
            {
                PageNumber--;
                GetBanknotes(PageNumber, PageSize);
            }
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
                TotalPages = _banknoteService.GetTotalPageNumber(PageSize);
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
                    TotalPages = _banknoteService.GetTotalPageNumber(PageSize);
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

    }
}
