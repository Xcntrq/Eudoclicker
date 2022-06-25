using nsMobData;
using UnityEngine;

namespace nsMobDataDefaultCube
{
    [CreateAssetMenu(menuName = "ScObs/DefaultMobData")]
    public class DefaultMobData : MobData
    {
        [SerializeField] private float _turnProbability;
        [SerializeField] private float _turnAngleLimit;
        [SerializeField] private float _turnSpeed;

        public float TurnProbability => _turnProbability;
        public float TurnAngleLimit => _turnAngleLimit;
        public float TurnSpeed => _turnSpeed;
    }
}
