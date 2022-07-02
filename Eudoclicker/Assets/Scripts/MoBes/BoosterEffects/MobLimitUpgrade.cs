using nsBoosterEffect;
using nsMobSpawner;
using UnityEngine;

namespace nsMobLimitUpgrade
{
    public class MobLimitUpgrade : BoosterEffect
    {
        [SerializeField] private MobSpawner _mobSpawner;
        [SerializeField] private int _amount;

        public override void Proc(out string message)
        {
            message = string.Concat('+', _amount, " to mob limit");
            _mobSpawner.IncreaseGameOverMobCount(_amount);
        }
    }
}
