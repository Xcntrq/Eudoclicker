using System;

namespace nsHighscoreItem
{
    [Serializable]
    public class HighscoreItem
    {
        private string _name;
        private int _score;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public HighscoreItem()
        {
            _name = string.Empty;
            _score = 0;
        }

        public HighscoreItem(string name, int score)
        {
            _name = name;
            _score = score;
        }

        public HighscoreItem(HighscoreItem highscoreItem)
        {
            _name = highscoreItem.Name;
            _score = highscoreItem.Score;
        }
    }
}
