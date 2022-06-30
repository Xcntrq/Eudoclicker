using nsICoins;
using nsMob;
using System;
using nsIKillable;
using nsILevelable;
using nsIDamagable;
using nsICoinDropper;
using nsIHealth;
using nsIMobBehaviour;

namespace nsSpiderMob
{
    public class SpiderMob : Mob, ILevelable, IKillable, IDamagable, ICoinDropper
    {
        private IHealth _health;
        private ICoins _coins;

        public event Action<int> OnCoinDrop;
        public event Action<IKillable> OnDeath;

        private void Awake()
        {
            _otherLevelables = GetOtherLevelables();
            _health = GetComponent<IHealth>();
            _coins = GetComponent<ICoins>();

            _mobBehaviours = GetComponents<IMobBehaviour>();
            SetMobBehavioursActive(false);
        }

        public void SetLevel(int level)
        {
            UpdateOtherLevelables(level);
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
