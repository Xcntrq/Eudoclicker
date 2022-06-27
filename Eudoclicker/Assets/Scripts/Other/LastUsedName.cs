using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace nsLastUsedName
{
    public class LastUsedName
    {
        private const string _fileName = "lastusedname.dat";

        private readonly string _filePath;
        private string _value;

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                WriteToFile();
            }
        }

        public LastUsedName()
        {
            _filePath = string.Concat(Application.persistentDataPath, '/', _fileName);
            bool isSaveFileFound = File.Exists(_filePath);
            _value = string.Empty;
            if (isSaveFileFound)
            {
                ReadFromFile();
            }
            else
            {
                WriteToFile();
            }
        }

        private void WriteToFile()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Create(_filePath);
            binaryFormatter.Serialize(fileStream, _value);
            fileStream.Close();
        }

        private void ReadFromFile()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(_filePath, FileMode.Open);
            if (binaryFormatter.Deserialize(fileStream) is string value)
            {
                _value = value;
            }
            fileStream.Close();
        }
    }
}
