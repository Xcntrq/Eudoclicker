using nsHealth;
using nsHealthBar;
using nsIKillable;
using nsMobData;
using System;
using UnityEngine;

namespace nsMob
{
    public abstract class Mob : MonoBehaviour, IKillable
    {
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private MobData _mobData;

        private Health _health;

        protected int _waveNumber;

        public event Action<Mob> OnDeath;

        public void Initialize(int waveNumber, Camera camera)
        {
            _waveNumber = waveNumber;
            int healthAmount = (int)(_mobData.MaxHealth * (1f + _waveNumber * _mobData.WaveNumberHealthBoost));
            _health = new Health(healthAmount);
            _healthBar.Initialize(_health, camera);
            PostInitialize();
        }

        protected virtual void PostInitialize()
        {

        }

        public void DecreaceHealth(int amount)
        {
            _health.Decrease(amount);
            if (_health.Value == 0) Kill();
        }

        public void Kill()
        {
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
