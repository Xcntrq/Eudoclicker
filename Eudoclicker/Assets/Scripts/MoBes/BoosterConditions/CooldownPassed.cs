using nsBoosterCondition;
using UnityEngine;

namespace nsCooldownPassed
{
    public class CooldownPassed : BoosterCondition
    {
        [SerializeField] private float _cooldownTime;

        private float _timeLeft;
        private bool _isOnCooldown;

        public override float ReadyPercentage
        {
            get { return Mathf.InverseLerp(_cooldownTime, 0f, _timeLeft); }
        }

        private void Awake()
        {
            _isOnCooldown = true;
            _timeLeft = _cooldownTime;
        }

        private void Update()
        {
            if (!_isOnCooldown) return;

            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0) _isOnCooldown = false;
        }

        public override bool IsSatisfied(out string message)
        {
            message = !_isOnCooldown ? string.Empty : string.Concat(_timeLeft.ToString("0.0"), " sec CD!");
            return !_isOnCooldown;
        }

        public override void Proc(out string message)
        {
            message = string.Concat(_cooldownTime.ToString("0.0"), " sec CD");
            _isOnCooldown = true;
            _timeLeft = _cooldownTime;
        }
    }
}
