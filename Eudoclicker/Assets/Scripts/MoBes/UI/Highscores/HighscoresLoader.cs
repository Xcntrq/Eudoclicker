using nsClearHighscoresButton;
using nsHighscoreItem;
using nsHighscores;
using TMPro;
using UnityEngine;

namespace nsHighscoresLoader
{
    public class HighscoresLoader : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _names;
        [SerializeField] private TextMeshProUGUI _scores;
        [SerializeField] private TextMeshProUGUI _empty;
        [SerializeField] private ClearHighscoresButton _clearHighscoresButton;

        private Highscores _highscores;

        private void Awake()
        {
            _highscores = new Highscores(false);
            RefreshGUI();
            ShowHighscores();
            _clearHighscoresButton.OnClick += ClearHighscoresButton_OnClick;
        }

        private void ClearHighscoresButton_OnClick()
        {
            _highscores = new Highscores(true);
            RefreshGUI();
            ShowHighscores();
        }

        private void RefreshGUI()
        {
            if (_highscores.ItemCount != 0)
            {
                _names.gameObject.SetActive(true);
                _scores.gameObject.SetActive(true);
                _empty.gameObject.SetActive(false);

            }
            else
            {
                _names.gameObject.SetActive(false);
                _scores.gameObject.SetActive(false);
                _empty.gameObject.SetActive(true);
            }
        }

        private void ShowHighscores()
        {
            string names = string.Empty;
            string scores = string.Empty;
            foreach (HighscoreItem highscoreItem in _highscores.LoadedList)
            {
                if (names != string.Empty) names = string.Concat(names, "<br>");
                if (scores != string.Empty) scores = string.Concat(scores, "<br>");
                names = string.Concat(names, highscoreItem.Name);
                scores = string.Concat(scores, highscoreItem.Score);
            }
            _names.SetText(names);
            _scores.SetText(scores);
        }
    }
}
