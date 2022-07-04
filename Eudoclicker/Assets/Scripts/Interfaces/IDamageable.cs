using nsOnHealthDecreaceEventArgs;
using System;

namespace nsIDamageable
{
    public interface IDamageable
    {
        public event Action<OnHealthDecreaceEventArgs> OnHealthDecreace;

        public void DecreaceHealth(int amount);
    }
}
