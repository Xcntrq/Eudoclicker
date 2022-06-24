using UnityEngine;

namespace nsMobData
{
    [CreateAssetMenu(menuName = "ScObs/MobDatas/MobData")]
    public class MobData : ScriptableObject
    {
        [SerializeField] private float _speed;

        public float Speed => _speed;
    }
}
