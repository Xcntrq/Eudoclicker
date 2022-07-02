using nsIDamage;
using System;
using UnityEngine;

namespace nsDamage
{
    public class Damage : MonoBehaviour, IDamage
    {
        [SerializeField] private int _startingDamage;

        private int _value;

        public bool Infinite { get; set; }

        public int Value => Infinite ? int.MaxValue : _value;

        public event Action<int> OnValueChange;

        private void Awake()
        {
            Infinite = false;
            _value = _startingDamage;
        }

        private void Start()
        {
            OnValueChange?.Invoke(_value);
        }

        public void IncreaseBy(int amount)
        {
            _value += amount;
            OnValueChange?.Invoke(_value);
        }
    }
}
