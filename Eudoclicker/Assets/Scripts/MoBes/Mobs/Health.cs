using nsHealthData;
using nsIHealth;
using nsIHealthValue;
using nsILevelable;
using System;
using UnityEngine;

namespace nsHealth
{
    public class Health : MonoBehaviour, IHealth, IHealthValue, ILevelable
    {
        [SerializeField] private HealthData _healthData;

        private int _maxValue;
        private int _value;

        public int Value => _value;
        private float ValueNormalized => (float)_value / _maxValue;

        public event Action<float> OnValueChange;

        public void SetLevel(int level)
        {
            _maxValue = (int)(_healthData.MaxHealth * (1f + level * _healthData.LevelBonus));
            _value = _maxValue;
            OnValueChange?.Invoke(ValueNormalized);
        }

        public void Decrease(int amount)
        {
            _value = Mathf.Clamp(_value - amount, 0, _value);
            OnValueChange?.Invoke(ValueNormalized);
        }
    }
}
