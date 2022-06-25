using nsPlayerKillCount;
using TMPro;
using UnityEngine;

namespace nsKillCountText
{
    public class KillCountText : MonoBehaviour
    {
        [SerializeField] private PlayerKillCount _playerKillCount;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _playerKillCount.OnValueChange += PlayerKillCount_OnValueChange;
        }

        private void PlayerKillCount_OnValueChange(string text)
        {
            _text.SetText(text);
        }
    }
}
