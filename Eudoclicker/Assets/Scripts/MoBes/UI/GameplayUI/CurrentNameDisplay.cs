using nsPlayerName;
using TMPro;
using UnityEngine;

namespace nsCurrentNameDisplay
{
    public class CurrentNameDisplay : MonoBehaviour
    {
        [SerializeField] private PlayerName _playerName;
        [SerializeField] private string _preText;
        [SerializeField] private string _textWhenEmpty;
        [SerializeField] private bool _activeWhenEmpty;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _playerName.OnPlayerNameChange += PlayerName_OnPlayerNameChange;
        }

        private void PlayerName_OnPlayerNameChange(string playerName)
        {
            bool isPlayerNameEmpty = playerName == string.Empty;
            bool isNeeded = !isPlayerNameEmpty || _activeWhenEmpty;
            gameObject.SetActive(isNeeded);
            string text = isPlayerNameEmpty ? _textWhenEmpty : string.Concat(_preText, playerName);
            _text.SetText(text);
        }
    }
}
