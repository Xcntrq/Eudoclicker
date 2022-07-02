using nsBoosterEffect;
using nsDamage;
using nsIDamageable;
using nsMobSpawner;
using System.Collections.Generic;
using UnityEngine;

namespace nsHitAllMobs
{
    public class HitAllMobs : BoosterEffect
    {
        [SerializeField] private MobSpawner _mobSpawner;
        [SerializeField] private Damage _damage;

        public override void Proc(out string message)
        {
            message = string.Concat("All mobs hit");
            List<IDamageable> damageables = new List<IDamageable>();
            foreach (IDamageable damageable in _mobSpawner.DamageableMobs)
            {
                damageables.Add(damageable);
            }
            //The list doesn't care if they die from this damage because we're going backwards
            for (int i = damageables.Count - 1; i >= 0; i--)
            {
                damageables[i].DecreaceHealth(_damage.Value);
            }
        }
    }
}
