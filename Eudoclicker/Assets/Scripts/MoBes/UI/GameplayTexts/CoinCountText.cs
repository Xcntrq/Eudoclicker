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

        private void PlayerCoinCount_OnValueChange(int coinAmount)
        {
            _text.SetText(coinAmount.ToString());
        }
    }
}
