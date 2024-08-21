using Numismatics.CORE.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Repositories
{
    public class Repository<T> where T : new()
    {
        protected string _filePath = @"../../../../Numismatics.CORE/Data/{0}";
        protected readonly Serializer _serializer = new();
        protected void SetFileName(string fileName)
        {
            _filePath = string.Format(_filePath, fileName);

        }
        protected List<T> Load()
        {
            return _serializer.FromFile<List<T>>(_filePath)
                ?? new List<T>();

        }
        protected void Save(List<T> instances)
        {
            _serializer.ToFile(instances, _filePath);
        }
    }
}
