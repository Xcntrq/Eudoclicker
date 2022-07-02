using nsCoinValueProvider;
using nsIDamage;
using System;

namespace nsDamageIsCoinValue
{
    public class DamageIsCoinValue : CoinValueProvider
    {
        private IDamage _damage;

        public override event Action<int> OnValueChange;

        private void Awake()
        {
            //DamageIsCoinValue is better off parented to an IDamage gameObject
            //Because you may want a few CoinValueProviders based on the same IDamage
            //They will be easier to select from the inspector if they all are
            //separate gameObjects parented to that IDamage, hence "InParent"
            _damage = GetComponentInParent<IDamage>();
            _damage.OnValueChange += Damage_OnValueChange;
        }

        private void Damage_OnValueChange(int value)
        {
            OnValueChange?.Invoke(value);
        }
    }
}
