using nsCoinValueProvider;
using nsMobSpawner;
using System;

namespace nsMobLimitIsCoinValue
{
    public class MobLimitIsCoinValue : CoinValueProvider
    {
        private MobSpawner _mobSpawner;

        public override event Action<int> OnValueChange;

        private void Awake()
        {
            //MobLimitIsCoinValue is better off parented to a MobSpawner gameObject
            //Because you may want a few CoinValueProviders based on the same MobSpawner
            //They will be easier to select from the inspector if they all are
            //separate gameObjects parented to that MobSpawner, hence "InParent"
            _mobSpawner = GetComponentInParent<MobSpawner>();
            _mobSpawner.OnMobCountChange += MobSpawner_OnMobCountChange;
        }

        private void MobSpawner_OnMobCountChange(int arg1, int arg2)
        {
            OnValueChange?.Invoke(arg2);
        }
    }
}
