using UnityEngine;

namespace nsDefaultMovementData
{
    [CreateAssetMenu(menuName = "ScObs/DefaultMovementData")]
    public class DefaultMovementData : ScriptableObject
    {
        [Header("e.g., 0.2 gives 20% per level")]
        [SerializeField] private float _levelBonus;
        [SerializeField] private float _speed;
        [SerializeField] private float _turnProbability;
        [SerializeField] private float _turnAngleLimit;
        [SerializeField] private float _turnSpeed;

        public float Speed => _speed;
        public float LevelBonus => _levelBonus;
        public float TurnProbability => _turnProbability;
        public float TurnAngleLimit => _turnAngleLimit;
        public float TurnSpeed => _turnSpeed;
    }
}
