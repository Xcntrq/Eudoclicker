using UnityEngine;

namespace nsBoosterCondition
{
    public abstract class BoosterCondition : MonoBehaviour
    {
        public abstract float ReadyPercentage { get; }

        public abstract bool IsSatisfied(out string message);

        public abstract void Proc(out string message);
    }
}
