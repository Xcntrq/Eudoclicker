using nsICoins;
using nsISpawnFinisher;
using nsMob;
using System;
using nsICoinDropper;
using nsIKillable;
using nsIMobBehaviour;
using nsIHealth;
using nsIDamageable;
using nsILevelable;
using nsIFreezable;
using UnityEngine;

namespace nsSlimeMob
{
    public class SlimeMob : Mob, ILevelable, IKillable, IDamageable, IFreezable, ICoinDropper
    {
        private Animator _animator;
        private ISpawnFinisher _spawnFinisher;
        private IHealth _health;
        private ICoins _coins;

        private bool _isFrozen;
        private bool _hasLeveled;
        private bool _hasSpawnFinished;

        public event Action<int> OnCoinDrop;
        public event Action<IKillable> OnDeath;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _otherLevelables = GetOtherLevelables();
            _health = GetComponent<IHealth>();
            _coins = GetComponent<ICoins>();

            _spawnFinisher = GetComponentInChildren<ISpawnFinisher>();
            _spawnFinisher.OnSpawnFinish += SpawnFinisher_OnSpawnFinish;

            _mobBehaviours = GetComponents<IMobBehaviour>();
            SetMobBehavioursActive(false);

            _hasSpawnFinished = false;
            _hasLeveled = false;
            _isFrozen = false;
        }

        public void SetLevel(int level)
        {
            UpdateOtherLevelables(level);
            SetMobBehavioursActive(!_isFrozen && _hasSpawnFinished);
            _hasLeveled = true;
        }

        private void SpawnFinisher_OnSpawnFinish()
        {
            SetMobBehavioursActive(!_isFrozen && _hasLeveled);
            _hasSpawnFinished = true;
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
            SetMobBehavioursActive(_hasSpawnFinished && _hasLeveled);
            _isFrozen = false;
        }
    }
}
