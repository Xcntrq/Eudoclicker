using System;

namespace nsIKillable
{
    public interface IKillable
    {
        public event Action<IKillable> OnDeath;

        public void Kill();
    }
}
