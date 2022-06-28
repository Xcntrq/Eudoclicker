using nsBoosterEffect;
using nsMobSpawner;
using UnityEngine;

namespace nsAddSeconds
{
    public class AddSeconds : BoosterEffect
    {
        [SerializeField] private MobSpawner _mobSpawner;
        [SerializeField] private int _amount;

        public override void Proc(out string message)
        {
            message = string.Concat("+", _amount, " sec to spawn");
            _mobSpawner.AddSeconds(_amount);
        }
    }
}
