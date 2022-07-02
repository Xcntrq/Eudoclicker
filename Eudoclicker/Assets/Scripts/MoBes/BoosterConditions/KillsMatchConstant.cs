using nsBoosterCondition;
using nsPlayerKillCount;
using UnityEngine;

namespace nsKillsMatchConstant
{
    public class KillsMatchConstant : BoosterCondition
    {
        [SerializeField] private PlayerKillCount _playerKillCount;
        [SerializeField] private int _killsRequired;

        public override float ReadyPercentage
        {
            get { return Mathf.InverseLerp(0f, _killsRequired, _playerKillCount.Value); }
        }

        public override bool IsSatisfied(out string message)
        {
            bool result = _playerKillCount.Value >= _killsRequired;
            message = result ? string.Empty : string.Concat("Need ", _killsRequired, " kills!");
            return result;
        }

        public override void Proc(out string message)
        {
            message = string.Empty;
        }
    }
}
