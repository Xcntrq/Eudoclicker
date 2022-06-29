using UniRandom = UnityEngine.Random;
using SysRandom = System.Random;
using nsCoinDropperData;
using nsICoinDropper;
using nsIntValue;
using nsMob;
using UnityEngine;
using System;
using nsISpeedCarrier;
using nsISpeedProvider;

namespace nsSpiderMob
{
    public class SpiderMob : Mob, ICoinDropper, ISpeedProvider
    {
        [SerializeField] private CoinDropperData _coinDropperData;
        [SerializeField] private IntValue _behaviourSeed;

        private SysRandom _behaviourRandom;
        private ISpeedCarrier _speedCarrier;

        public event Action OnCoinDrop;
        public event Action<float> OnSpeedChange;

        protected override void PostInitialize()
        {
            _speedCarrier = GetComponent<ISpeedCarrier>();
            _speedCarrier.OnSpeedChange += SpeedCarrier_OnSpeedChange;
            SetMobBehavioursActive(true);
            int randomInt = UniRandom.Range(int.MinValue, int.MaxValue);
            _behaviourRandom = _behaviourSeed.Value == 0 ? new SysRandom(randomInt) : new SysRandom(_behaviourSeed.Value);
        }

        private void SpeedCarrier_OnSpeedChange(float newSpeed)
        {
            OnSpeedChange?.Invoke(newSpeed);
        }

        protected override void PreDeath()
        {
            float chance;
            for (chance = _coinDropperData.CoinChance; chance >= 1f; chance--)
            {
                OnCoinDrop?.Invoke();
            }
            if (IsCoinDropSuccessful(chance)) OnCoinDrop?.Invoke();
        }

        private bool IsCoinDropSuccessful(float chance)
        {
            float _coinDropDecider = 1f - (float)_behaviourRandom.NextDouble();
            return (chance > 0f) && (_coinDropDecider <= chance);
        }
    }
}
