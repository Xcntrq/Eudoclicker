using UniRandom = UnityEngine.Random;
using SysRandom = System.Random;
using nsCoinsData;
using nsIntValue;
using UnityEngine;
using nsICoins;

namespace nsCoins
{
    public class Coins : MonoBehaviour, ICoins
    {
        [SerializeField] private CoinsData _coinsData;
        [SerializeField] private IntValue _behaviourSeed;

        private SysRandom _behaviourRandom;

        private void Awake()
        {
            int randomInt = UniRandom.Range(int.MinValue, int.MaxValue);
            _behaviourRandom = _behaviourSeed.Value == 0 ? new SysRandom(randomInt) : new SysRandom(_behaviourSeed.Value);
        }

        public int Drop()
        {
            float chance;
            int coinAmount = 0;
            for (chance = _coinsData.CoinChance; chance >= 1f; chance--)
            {
                coinAmount++;
            }
            coinAmount += FinalDrop(chance);
            return coinAmount;
        }

        private int FinalDrop(float chance)
        {
            float _coinDropDecider = 1f - (float)_behaviourRandom.NextDouble();
            return (chance > 0f) && (_coinDropDecider <= chance) ? 1 : 0;
        }
    }
}
