using nsBoosterEffect;
using nsDamage;
using UnityEngine;

namespace nsDamageUpgrade
{
    public class DamageUpgrade : BoosterEffect
    {
        [SerializeField] private Damage _damage;
        [SerializeField] private int _amount;

        public override void Proc(out string message)
        {
            message = string.Concat('+', _amount, " to your damage");
            _damage.IncreaseBy(_amount);
        }
    }
}
