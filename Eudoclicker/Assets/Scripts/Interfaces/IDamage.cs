using System;

namespace nsIDamage
{
    public interface IDamage
    {
        public bool Infinite { get; set; }

        public int Value { get; }

        public event Action<int> OnValueChange;
    }
}
