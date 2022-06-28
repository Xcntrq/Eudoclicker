using nsBoosterCondition;
using nsPlayerCoinCount;
using UnityEngine;

namespace nsEnoughCoins
{
    public class EnoughCoins : BoosterCondition
    {
        [SerializeField] private PlayerCoinCount _playerCoinCount;
        [SerializeField] private int _coinPrice;

        public override float ReadyPercentage
        {
            get { return Mathf.InverseLerp(0f, _coinPrice, _playerCoinCount.Value); }
        }

        public override bool IsSatisfied(out string message)
        {
            bool result = _playerCoinCount.Value >= _coinPrice;
            message = result ? string.Empty : string.Concat("Need ", _coinPrice, " coins!");
            return result;
        }

        public override void Proc(out string message)
        {
            message = string.Concat("-", _coinPrice, " coins");
            _playerCoinCount.ReduceCoinCount(_coinPrice);
        }
    }
}
