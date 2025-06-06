using Numismatics.CORE.Serialization.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numismatics.CORE.Serialization
{
   
    public class Serializer
    {
        private ISerialization _serialization;

        public Serializer(ISerialization serialization)
        {
            _serialization = serialization;
        }
        public void HandleFile(string path)
        {
            if (!File.Exists(path))
            {
                var stream = File.Create(path);
                stream.Close();
            }
        }
        public G? FromFile<G>(string fileName)
        {
            HandleFile(fileName);
            string jsonString = File.ReadAllText(fileName);
            return _serialization.Deserialize<G>(jsonString);
        }
        public void ToFile<G>(G objects, string fileName)
        {
            HandleFile(fileName);
            string serializedObjects = _serialization.Serialize(objects);
            var streamWriter = new StreamWriter(fileName);
            streamWriter.Write(serializedObjects);
            streamWriter.Close();
        }
    }
}
