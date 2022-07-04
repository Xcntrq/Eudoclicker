using nsIPageNumber;
using System;
using UnityEngine;

namespace nsPageNumber
{
    public class PageNumber : MonoBehaviour, IPageNumber
    {
        private int _value;

        public int MinValue { get; private set; }
        public int MaxValue { get; private set; }

        public event Action<int> OnValueChange;

        public void Init(int minValue, int value, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            _value = value;
            OnValueChange?.Invoke(_value);
        }

        public void Decrease()
        {
            if (_value > MinValue) OnValueChange?.Invoke(--_value);
        }

        public void Increase()
        {
            if (_value < MaxValue) OnValueChange?.Invoke(++_value);
        }
    }
}
