using UnityEngine;

namespace nsBoundValue
{
    [CreateAssetMenu(menuName = "ScObs/BoundsValue")]
    public class BoundsValue : ScriptableObject
    {
        [SerializeField] private Bounds _value;

        public Bounds Value => _value;
    }
}
