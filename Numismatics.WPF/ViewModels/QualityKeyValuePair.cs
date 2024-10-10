using Numismatics.CORE.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.WPF.ViewModels
{
    public class QualityKeyValuePair<K,V>
    {
        public K Key { get; set; }
        public V Value { get; set; }

        public QualityKeyValuePair() { }
        public QualityKeyValuePair(K key, V value)
        {
            Key = key;
            Value = value;
        }
    }
}
