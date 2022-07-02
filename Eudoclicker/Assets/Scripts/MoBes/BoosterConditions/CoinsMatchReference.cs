using nsBoosterCondition;
using nsCoinValueProvider;
using nsPlayerCoinCount;
using UnityEngine;

namespace nsCoinsMatchReference
{
    public class CoinsMatchReference : BoosterCondition
    {
        [SerializeField] private PlayerCoinCount _playerCoinCount;
        [SerializeField] private CoinValueProvider _coinValueProvider;
        [SerializeField] private float _multiplier;

        private int _totalPrice;

        private void Awake()
        {
            _coinValueProvider.OnValueChange += CoinValueProvider_OnValueChange;
        }

        private void CoinValueProvider_OnValueChange(int value)
        {
            _totalPrice = (int)(value * _multiplier);
        }

        public override float ReadyPercentage
        {
            get
            {
                return Mathf.InverseLerp(0f, _totalPrice, _playerCoinCount.Value);
            }
        }

        public override bool IsSatisfied(out string message)
        {
            bool result = _playerCoinCount.Value >= _totalPrice;
            message = result ? string.Empty : string.Concat("Need ", _totalPrice, " coins!");
            return result;
        }

        public override void Proc(out string message)
        {
            message = string.Concat('-', _totalPrice, " coins");
            _playerCoinCount.ReduceCoinCount(_totalPrice);
        }
    }
}
