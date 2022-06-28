using System;
using UnityEngine;

namespace nsHealth
{
    public class Health
    {
        private readonly int _maxValue;
        private int _value;

        public int Value => _value;
        private float ValueNormalized => (float)_value / _maxValue;

        public event Action<float> OnValueChange;

        public Health(int maxHealth)
        {
            _maxValue = maxHealth;
            _value = maxHealth;
            OnValueChange?.Invoke(ValueNormalized);
        }

        public void Decrease(int value)
        {
            _value = Mathf.Clamp(_value - value, 0, _value);
            OnValueChange?.Invoke(ValueNormalized);
        }
    }
}
