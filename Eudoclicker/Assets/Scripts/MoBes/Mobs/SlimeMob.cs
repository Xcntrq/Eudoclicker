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
            if (IsCoinDropSuccessful()) OnCoinDrop?.Invoke();
        }

        private bool IsCoinDropSuccessful()
        {
            float _coinDropDecider = 1f - (float)_behaviourRandom.NextDouble();
            return (_coinDropperData.CoinChance > 0f) && (_coinDropDecider <= _coinDropperData.CoinChance);
        }
    }
}
