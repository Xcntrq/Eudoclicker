using nsBoosterCondition;
using nsICooldownCondition;
using UnityEngine;

namespace nsCooldownFromConstant
{
    public class CooldownFromConstant : BoosterCondition, ICooldownCondition
    {
        [SerializeField] private float _cooldownTime;
        [SerializeField] private bool _isAvailableAtStart;

        private float _timeLeft;
        private bool _isOnCooldown;

        public float SecondsLeft => _timeLeft;

        public override float ReadyPercentage
        {
            get { return Mathf.InverseLerp(_cooldownTime, 0f, _timeLeft); }
        }

        private void Awake()
        {
            _isOnCooldown = !_isAvailableAtStart;
            _timeLeft = _isOnCooldown ? _cooldownTime : 0f;
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
