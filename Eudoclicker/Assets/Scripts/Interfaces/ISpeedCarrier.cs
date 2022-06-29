using System;

namespace nsISpeedCarrier
{
    public interface ISpeedCarrier
    {
        public event Action<float> OnSpeedChange;
    }
}
