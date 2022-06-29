using UniRandom = UnityEngine.Random;
using SysRandom = System.Random;
using nsCoinDropperData;
using nsICoinDropper;
using nsISpawnFinishWaiter;
using nsMob;
using System;
using UnityEngine;
using nsIntValue;

namespace nsSlimeMob
{
    public class SlimeMob : Mob, ICoinDropper, ISpawnFinishWaiter
    {
        [SerializeField] private CoinDropperData _coinDropperData;
        [SerializeField] private IntValue _behaviourSeed;

        private SysRandom _behaviourRandom;

        public event Action OnCoinDrop;

        public void OnSpawnFinish()
        {
            SetMobBehavioursActive(true);
        }

        protected override void PostInitialize()
        {
            int randomInt = UniRandom.Range(int.MinValue, int.MaxValue);
            _behaviourRandom = _behaviourSeed.Value == 0 ? new SysRandom(randomInt) : new SysRandom(_behaviourSeed.Value);
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
