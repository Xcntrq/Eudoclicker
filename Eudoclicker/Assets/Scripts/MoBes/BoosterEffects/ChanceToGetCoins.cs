using UniRandom = UnityEngine.Random;
using SysRandom = System.Random;
using nsBoosterEffect;
using nsPlayerCoinCount;
using UnityEngine;
using nsIntValue;

namespace nsChanceToGetCoins
{
    public class ChanceToGetCoins : BoosterEffect
    {
        [SerializeField] private PlayerCoinCount _playerCoinCount;
        [SerializeField] private float _chance;
        [SerializeField] private int _amountPerTry;
        [SerializeField] private int _timesRepeated;
        [SerializeField] private IntValue _behaviourSeed;

        private SysRandom _behaviourRandom;

        private void Awake()
        {
            int randomInt = UniRandom.Range(int.MinValue, int.MaxValue);
            _behaviourRandom = _behaviourSeed.Value == 0 ? new SysRandom(randomInt) : new SysRandom(_behaviourSeed.Value);
        }

        public override void Proc(out string message)
        {
            int totalAmount = 0;
            for (int i = 0; i < _timesRepeated; i++)
            {
                totalAmount += _amountPerTry * CoinFromChance(_chance);
            }
            message = string.Concat('+', totalAmount, " coins");
            _playerCoinCount.IncreaceCoinCount(totalAmount);
        }

        private int CoinFromChance(float chance)
        {
            float _coinDropDecider = 1f - (float)_behaviourRandom.NextDouble();
            return (chance > 0f) && (_coinDropDecider <= chance) ? 1 : 0;
        }
    }
}
