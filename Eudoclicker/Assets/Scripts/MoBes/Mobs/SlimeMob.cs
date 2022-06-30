using nsICoins;
using nsISpawnFinisher;
using nsMob;
using System;
using nsICoinDropper;
using nsIKillable;
using nsIMobBehaviour;
using nsIHealth;
using nsIDamagable;
using nsILevelable;

namespace nsSlimeMob
{
    public class SlimeMob : Mob, ILevelable, IKillable, IDamagable, ICoinDropper
    {
        private ISpawnFinisher _spawnFinisher;
        private IHealth _health;
        private ICoins _coins;

        public event Action<int> OnCoinDrop;
        public event Action<IKillable> OnDeath;

        private void Awake()
        {
            _otherLevelables = GetOtherLevelables();
            _health = GetComponent<IHealth>();
            _coins = GetComponent<ICoins>();

            _spawnFinisher = GetComponentInChildren<ISpawnFinisher>();
            _spawnFinisher.OnSpawnFinish += SpawnFinisher_OnSpawnFinish;

            _mobBehaviours = GetComponents<IMobBehaviour>();
            SetMobBehavioursActive(false);
        }

        public void SetLevel(int level)
        {
            UpdateOtherLevelables(level);
        }

        private void SpawnFinisher_OnSpawnFinish()
        {
            SetMobBehavioursActive(true);
        }

        public void Kill()
        {
            int coinsDropped = _coins.Drop();
            OnCoinDrop?.Invoke(coinsDropped);
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }

        public void DecreaceHealth(int amount)
        {
            _health.Decrease(amount);
            if (_health.Value == 0) Kill();
        }
    }
}
