using System;

namespace nsIMovementSpeedChanger
{
    public interface IMovementSpeedChanger
    {
        public event Action<float> OnSpeedChange;
    }
}
