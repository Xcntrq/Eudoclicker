using nsBoosterEffect;
using nsIKillable;
using nsIMuteable;
using nsMobSpawner;
using System.Collections.Generic;
using UnityEngine;

namespace nsKillAllMobs
{
    public class KillAllMobs : BoosterEffect
    {
        [SerializeField] private MobSpawner _mobSpawner;
        [SerializeField] private bool _muteMobs;

        public override void Proc(out string message)
        {
            message = string.Concat("All mobs killed");
            List<IKillable> killables = new List<IKillable>();
            foreach (IKillable killable in _mobSpawner.KillableMobs)
            {
                killables.Add(killable);
            }
            //The list doesn't care if they die because we're going backwards
            for (int i = killables.Count - 1; i >= 0; i--)
            {
                if (_muteMobs && (killables[i] is IMuteable muteable)) muteable.Mute();
                killables[i].Kill();
            }
        }
    }
}
