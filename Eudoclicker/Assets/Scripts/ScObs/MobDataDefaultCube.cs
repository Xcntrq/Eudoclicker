using nsMobData;
using UnityEngine;

namespace nsMobDataDefaultCube
{
    [CreateAssetMenu(menuName = "ScObs/MobDatas/MobDataDefaultCube")]
    public class MobDataDefaultCube : MobData
    {
        [SerializeField] private float _turnProbability;
        [SerializeField] private float _turnAngleLimit;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private Bounds _movementBounds;

        public float TurnProbability => _turnProbability;
        public float TurnAngleLimit => _turnAngleLimit;
        public float TurnSpeed => _turnSpeed;
        public Bounds MovementBounds => _movementBounds;
    }
}
