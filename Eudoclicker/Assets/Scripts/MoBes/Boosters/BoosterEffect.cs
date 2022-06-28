using UnityEngine;

namespace nsBoosterEffect
{
    public abstract class BoosterEffect : MonoBehaviour
    {
        public abstract void Proc(out string message);
    }
}
