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
        private readonly string _dataFolderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        private string _filePath;
        protected readonly Serializer _serializer = new();
        protected void SetFileName(string fileName)
        {
            if (!System.IO.Directory.Exists(_dataFolderPath))
            {
                System.IO.Directory.CreateDirectory(_dataFolderPath);
            }
            _filePath = System.IO.Path.Combine(_dataFolderPath, fileName);

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
