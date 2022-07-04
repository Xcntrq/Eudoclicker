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
using nsMobDeathData;
using nsOnDeathEventArgs;
using nsOnCoinDropEventArgs;
using nsOnHealthDecreaceEventArgs;
using nsOnPointGiveEventArgs;
using nsIPointsGiver;
using nsIPoints;
using nsIMuteable;

namespace nsSpiderMob
{
    public class SpiderMob : Mob, ILevelable, IKillable, IDamageable, IFreezable, ICoinDropper, IPointsGiver, IMuteable
    {
        [SerializeField] private MobDeathData _mobDeathData;

        private Animator _animator;
        private IHealth _health;
        private IPoints _points;
        private ICoins _coins;

        private bool _isMuted;
        private bool _isFrozen;
        private bool _hasLeveled;

        public event Action<OnDeathEventArgs> OnDeath;
        public event Action<OnCoinDropEventArgs> OnCoinDrop;
        public event Action<OnPointsGiveEventArgs> OnPointsGive;
        public event Action<OnHealthDecreaceEventArgs> OnHealthDecreace;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _otherLevelables = GetOtherLevelables();
            _health = GetComponent<IHealth>();
            _points = GetComponent<IPoints>();
            _coins = GetComponent<ICoins>();

            _mobBehaviours = GetComponents<IMobBehaviour>();
            SetMobBehavioursActive(false);

            _hasLeveled = false;
            _isFrozen = false;
            _isMuted = false;
        }

        public void SetLevel(int level)
        {
            UpdateOtherLevelables(level);
            SetMobBehavioursActive(!_isFrozen);
            _hasLeveled = true;
        }

        public void Kill()
        {
            OnCoinDrop?.Invoke(_coins.Drop());
            OnPointsGive?.Invoke(_points.Give());
            OnDeathEventArgs onDeathEventArgs = new OnDeathEventArgs(this, _isMuted ? null : _mobDeathData.DeathClip);
            OnDeath?.Invoke(onDeathEventArgs);
            Destroy(gameObject);
        }

        public void DecreaceHealth(int amount)
        {
            OnHealthDecreaceEventArgs onHealthDecreaceEventArgs = _health.Decrease(amount, !_isMuted);
            if (_health.Value != 0) OnHealthDecreace?.Invoke(onHealthDecreaceEventArgs);
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

        public void Mute()
        {
            _isMuted = true;
        }

        public void Unmute()
        {
            _isMuted = false;
        }
    }
}
