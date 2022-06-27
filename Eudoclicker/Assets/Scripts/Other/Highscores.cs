using nsHighscoreItem;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace nsHighscores
{
    public class Highscores
    {
        private const string _fileName = "highscores.dat";
        private const int _maxItemCount = 10;

        private readonly string _filePath;
        private List<HighscoreItem> _loadedList;

        public int ItemCount => _loadedList.Count;

        public List<HighscoreItem> LoadedList => _loadedList;

        public Highscores(bool removeExistingFile)
        {
            _filePath = string.Concat(Application.persistentDataPath, '/', _fileName);
            if (removeExistingFile) File.Delete(_filePath);
            bool isSaveFileFound = File.Exists(_filePath);
            _loadedList = new List<HighscoreItem>();
            if (isSaveFileFound)
            {
                ReadFromFile();
            }
            else
            {
                WriteToFile(_loadedList);
            }
        }

        public void SavePlayerHighscore(HighscoreItem playerHighscore)
        {
            int i;
            bool isAdded = false;
            List<HighscoreItem> _listToSave = new List<HighscoreItem>();

            for (i = 0; i < _loadedList.Count; i++)
            {
                if (playerHighscore.Score > _loadedList[i].Score)
                {
                    _listToSave.Add(playerHighscore);
                    isAdded = true;
                    break;
                }
                _listToSave.Add(_loadedList[i]);
            }

            for (; i < _loadedList.Count; i++)
            {
                if (_listToSave.Count >= _maxItemCount) break;
                _listToSave.Add(_loadedList[i]);
            }

            if ((_listToSave.Count < _maxItemCount) && (!isAdded)) _listToSave.Add(playerHighscore);

            WriteToFile(_listToSave);
        }

        private void WriteToFile(List<HighscoreItem> listToWrite)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Create(_filePath);
            binaryFormatter.Serialize(fileStream, listToWrite);
            fileStream.Close();
        }

        private void ReadFromFile()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(_filePath, FileMode.Open);
            if (binaryFormatter.Deserialize(fileStream) is List<HighscoreItem> loadedList)
            {
                _loadedList = loadedList;
            }
            fileStream.Close();
        }
    }
}
