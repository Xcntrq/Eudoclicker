using System;

namespace nsISpeedProvider
{
    public interface ISpeedProvider
    {
        public event Action<float> OnSpeedChange;
    }
}
