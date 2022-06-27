using nsPlayerName;
using TMPro;
using UnityEngine;

namespace nsNameInputField
{
    public class NameInputField : MonoBehaviour
    {
        [SerializeField] private PlayerName _playerName;

        private TMP_InputField _inputField;

        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
        }

        private void OnEnable()
        {
            _inputField.text = _playerName.Value;
        }
    }
}
