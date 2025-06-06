using Numismatics.CORE.Serialization;
using Numismatics.CORE.Serialization.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Repositories
{
    public class JSONRepository<T> where T : new()
    {
        private readonly string _dataFolderPath = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "NumismaticsAppData",
            "Data"
        );
        private string _filePath;
        protected readonly Serializer _serializer;


        public JSONRepository(ISerialization serialization)
        {
            _serializer = new Serializer(serialization);
        }
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
