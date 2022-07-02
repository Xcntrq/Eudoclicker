using nsICoins;
using nsMob;
using System;
using nsIKillable;
using nsILevelable;
using nsIDamageable;
using nsICoinDropper;
using nsIHealth;
using nsIMobBehaviour;
using nsIFreezable;
using UnityEngine;

namespace nsSpiderMob
{
    public class SpiderMob : Mob, ILevelable, IKillable, IDamageable, IFreezable, ICoinDropper
    {
        private Animator _animator;
        private IHealth _health;
        private ICoins _coins;

        private bool _isFrozen;
        private bool _hasLeveled;

        public event Action<int> OnCoinDrop;
        public event Action<IKillable> OnDeath;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _otherLevelables = GetOtherLevelables();
            _health = GetComponent<IHealth>();
            _coins = GetComponent<ICoins>();

            _mobBehaviours = GetComponents<IMobBehaviour>();
            SetMobBehavioursActive(false);

            _hasLeveled = false;
            _isFrozen = false;
        }

        public void SetLevel(int level)
        {
            UpdateOtherLevelables(level);
            SetMobBehavioursActive(!_isFrozen);
            _hasLeveled = true;
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

        public void Freeze()
        {
            _animator.SetFloat("speedMultiplier", 0f);
            SetMobBehavioursActive(false);
            _isFrozen = true;
        }

        public void Unfreeze()
        {
            _animator.SetFloat("speedMultiplier", 1f);
            SetMobBehavioursActive(_hasLeveled);
            _isFrozen = false;
        }
    }
}
