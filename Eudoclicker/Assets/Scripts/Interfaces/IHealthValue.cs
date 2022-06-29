using System;

namespace nsIHealthValue
{
    public interface IHealthValue
    {
        public event Action<float> OnValueChange;
    }
}
