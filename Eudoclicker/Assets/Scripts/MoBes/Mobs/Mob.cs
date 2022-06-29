using nsHealth;
using nsHealthBar;
using nsIKillable;
using nsIMobBehaviour;
using nsIWaveNumberHolder;
using nsMobData;
using System;
using UnityEngine;

namespace nsMob
{
    public abstract class Mob : MonoBehaviour, IWaveNumberCarrier, IKillable
    {
        [SerializeField] private MobData _mobData;
        [SerializeField] private HealthBar _healthBar;

        private IMobBehaviour[] _mobBehaviours;
        private Health _health;

        public int WaveNumber { get; private set; }

        public event Action<Mob> OnDeath;

        private void Awake()
        {
            _mobBehaviours = GetComponents<IMobBehaviour>();
            SetMobBehavioursActive(false);
        }

        public void Initialize(int waveNumber)
        {
            WaveNumber = waveNumber;
            int healthAmount = (int)(_mobData.MaxHealth * (1f + WaveNumber * _mobData.WaveNumberHealthBoost));
            _health = new Health(healthAmount);
            _healthBar.Initialize(_health);
            PostInitialize();
        }

        protected virtual void PostInitialize() { }

        public void DecreaceHealth(int amount)
        {
            _health.Decrease(amount);
            if (_health.Value == 0) Kill();
        }

        protected virtual void PreDeath() { }

        public void Kill()
        {
            PreDeath();
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }

        protected virtual void SetMobBehavioursActive(bool value)
        {
            foreach (IMobBehaviour mobBehaviour in _mobBehaviours)
            {
                mobBehaviour.SetComponentActive(value);
            }
        }
    }
}
