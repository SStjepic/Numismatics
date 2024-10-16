using Newtonsoft.Json.Linq;
using Numismatics.CORE.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Numismatics.WPF.ViewModels
{
    public class QualityKeyValuePair<K,V>: INotifyPropertyChanged
    {
        private K _key;
        private V _value;

        public K Key
        {
            get { return _key; }
            set
            {
                _key = value;
                OnPropertyChanged("Key");
            }
        }

        public V Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        public QualityKeyValuePair() { }
        public QualityKeyValuePair(K key, V value)
        {
            Key = key;
            Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
