using Numismatics.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModels.CurrencyViewModels
{
    public class CurrencyDataViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
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

        private string _mainUnitName;
        public string MainUnitName
        {
            get { return _mainUnitName; }
            set
            {
                _mainUnitName = value;
                OnPropertyChanged(nameof(MainUnitName));
            }
        }

        private string _subunitName;
        public string SubunitName
        {
            get { return _subunitName; }
            set
            {
                _subunitName = value;
                OnPropertyChanged(nameof(SubunitName));
            }
        }

        private int _subunitToMainUnit;
        public int SubunitToMainUnit
        {
            get { return _subunitToMainUnit; }
            set
            {
                _subunitToMainUnit = value;
                OnPropertyChanged(nameof(SubunitToMainUnit));
            }
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                if(value != null)
                {
                    _code = value.ToUpper();
                    OnPropertyChanged(nameof(Code));
                }
            }
        }

        public CurrencyDataViewModel() { }
        public CurrencyDataViewModel(CurrencyDTO currencyDTO)
        {
            if(currencyDTO == null || currencyDTO.Id == -1)
            {
                Id = -1;
            }
            else
            {
                Id = currencyDTO.Id;
                Name = currencyDTO.Name;
                MainUnitName = currencyDTO.MainUnitName;
                SubunitName = currencyDTO.SubunitName;
                SubunitToMainUnit = currencyDTO.SubunitToMainUnit;
                Code = currencyDTO.Code;
            }
        }

        public CurrencyDTO ToCurrencyDTO()
        {
            return new CurrencyDTO(Id, Name, MainUnitName, SubunitName, SubunitToMainUnit, Code);
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
                if (columnName == "Code")
                {
                    if (!string.IsNullOrEmpty(Code))
                    {
                        if(Code.Length != 3)
                        {
                            return "Currency code must contain 3 letters";
                        }
                    }
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Name", "MainUnitName","SubunitName", "Code", "Country" };

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
