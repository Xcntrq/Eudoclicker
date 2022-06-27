using nsPlayerName;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace nsAcceptNameButton
{
    public class AcceptNameButton : MonoBehaviour
    {
        [SerializeField] private PlayerName _playerName;
        [SerializeField] private TMP_InputField _inputField;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => _playerName.Value = _inputField.text);
        }
    }
}
