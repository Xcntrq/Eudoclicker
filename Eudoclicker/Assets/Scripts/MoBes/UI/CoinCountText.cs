using nsPlayerCoinCount;
using TMPro;
using UnityEngine;

namespace nsCoinCountText
{
    public class CoinCountText : MonoBehaviour
    {
        [SerializeField] private PlayerCoinCount _playerCoinCount;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _playerCoinCount.OnValueChange += PlayerCoinCount_OnValueChange;
        }

        private void PlayerCoinCount_OnValueChange(string text)
        {
            _text.SetText(text);
        }
    }
}
