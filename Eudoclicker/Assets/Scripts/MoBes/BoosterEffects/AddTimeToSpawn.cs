using nsBoosterEffect;
using nsMobSpawner;
using UnityEngine;

namespace nsAddTimeToSpawn
{
    public class AddTimeToSpawn : BoosterEffect
    {
        [SerializeField] private MobSpawner _mobSpawner;
        [SerializeField] private float _seconds;

        public override void Proc(out string message)
        {
            message = string.Concat('+', _seconds, " sec to spawn");
            _mobSpawner.AddTime(_seconds);
        }
    }
}
