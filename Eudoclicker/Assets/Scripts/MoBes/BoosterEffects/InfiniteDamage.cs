using nsBoosterEffect;
using nsDamage;
using nsMobSpawner;
using UnityEngine;

namespace nsInfiniteDamage
{
    public class InfiniteDamage : BoosterEffect
    {
        [SerializeField] private MobSpawner _mobSpawner;
        [SerializeField] private Damage _damage;

        private bool _isOnCooldown;
        private float _timeLeft;

        private void Awake()
        {
            _isOnCooldown = false;
        }

        private void Update()
        {
            if (!_isOnCooldown) return;

            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0f)
            {
                _isOnCooldown = false;
                _damage.Infinite = false;
            }
        }

        public override void Proc(out string message)
        {
            _isOnCooldown = true;
            _damage.Infinite = true;
            _timeLeft = _mobSpawner.TimeLeft;
            message = string.Concat("Duration = spawn time<br>(", _timeLeft.ToString("0.0"), " sec)");
        }
    }
}
