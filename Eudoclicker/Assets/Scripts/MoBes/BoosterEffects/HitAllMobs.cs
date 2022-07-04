using nsBoosterEffect;
using nsDamage;
using nsIDamageable;
using nsIMuteable;
using nsMobSpawner;
using System.Collections.Generic;
using UnityEngine;

namespace nsHitAllMobs
{
    public class HitAllMobs : BoosterEffect
    {
        [SerializeField] private MobSpawner _mobSpawner;
        [SerializeField] private Damage _damage;
        [SerializeField] private bool _muteMobs;

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
                IDamageable damageable = damageables[i];
                if (_muteMobs && (damageable is IMuteable muteable)) muteable.Mute();
                damageable.DecreaceHealth(_damage.Value);
                if (_muteMobs && (damageable != null) && (damageable is IMuteable unmuteable)) unmuteable.Unmute();
            }
        }
    }
}
