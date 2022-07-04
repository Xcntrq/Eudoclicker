using nsOnDeathEventArgs;
using System;

namespace nsIKillable
{
    public interface IKillable
    {
        public event Action<OnDeathEventArgs> OnDeath;

        public void Kill();
    }
}
