using Numismatics.CORE.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModel.CurrencyViewModel
{
    public class CurrencyDataViewModel : INotifyPropertyChanged, IDataErrorInfo
    {

        public CurrencyDataViewModel() { }
        public CurrencyDataViewModel(CurrencyDTO currencyDTO)
        {
            if (currencyDTO != null)
            {
                Id = currencyDTO.Id;
                Name = currencyDTO.Name;
                HunderthPartName = currencyDTO.HunderthPartName;
                Code = currencyDTO.Code;
            }

        }

        public CurrencyDTO ToCurrencyDTO()
        {
            return new CurrencyDTO(Id, Name, HunderthPartName, Code);
        }
        public long Id { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _hunderthPartName;
        public string HunderthPartName
        {
            get { return _hunderthPartName; }
            set
            {
                _hunderthPartName = value;
                OnPropertyChanged(nameof(HunderthPartName));
            }
        }
        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                OnPropertyChanged(nameof(Code));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                    {
                        return "Currency must have a name";
                    }
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Name", "HunderthPartName", "Code", "Country" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

    }
}
