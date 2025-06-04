using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Numismatics.WPF.ViewModels
{
    public class DisplayViewMode: INotifyPropertyChanged
    {
        public string PageIndicatorText => $"{PageNumber} / {TotalPages}";

        private int _pageNumber;
        public int PageNumber
        {
            get => _pageNumber;
            set
            {
                _pageNumber = value;
                OnPropertyChanged(nameof(PageNumber));
                OnPropertyChanged(nameof(PageIndicatorText)); 
            }
        }

        private int _totalPages;
        public int TotalPages
        {
            get => _totalPages;
            set
            {
                _totalPages = value;
                OnPropertyChanged(nameof(TotalPages));
                OnPropertyChanged(nameof(PageIndicatorText));
            }
        }

        private int _pageSize;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = value;
                OnPropertyChanged(nameof(PageSize));
            }
        }

        private int _totalItems;
        public int TotalItems
        {
            get => _totalItems;
            set 
            {
                _totalItems = value;
                OnPropertyChanged(nameof(TotalItems));
            }
        }
        public ICommand GetNextPageCommand { get; set; }
        public ICommand GetPreviousPageCommand {  get; set; }
        public ICommand GetTotalItemsNumberCommand {  get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand RefreshSearchCommand { get; set; }
        public virtual void GetNextPage() { }
        public virtual void GetPreviousPage() { }
        public virtual void GetTotalItemsNumber() { }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
