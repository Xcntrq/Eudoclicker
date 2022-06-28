using nsBoosterEffect;
using nsIKillable;
using nsMob;
using nsMobSpawner;
using System.Collections.Generic;
using UnityEngine;

namespace nsKillAllMobs
{
    public class KillAllMobs : BoosterEffect
    {
        [SerializeField] private MobSpawner _mobSpawner;

        public override void Proc(out string message)
        {
            message = string.Concat("All mobs killed");
            List<IKillable> killables = new List<IKillable>();
            foreach (IKillable killable in _mobSpawner.SpawnedMobs)
            {
                killables.Add(killable);
            }
            for (int i = killables.Count - 1; i >= 0; i--)
            {
                killables[i].Kill();
            }
        }
    }
}
