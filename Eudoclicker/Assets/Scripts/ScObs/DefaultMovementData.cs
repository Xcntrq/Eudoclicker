using UnityEngine;

namespace nsDefaultMovementData
{
    [CreateAssetMenu(menuName = "ScObs/DefaultMovementData")]
    public class DefaultMovementData : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _waveNumberSpeedBoost;
        [SerializeField] private float _turnProbability;
        [SerializeField] private float _turnAngleLimit;
        [SerializeField] private float _turnSpeed;

        public float Speed => _speed;
        public float WaveNumberSpeedBoost => _waveNumberSpeedBoost;
        public float TurnProbability => _turnProbability;
        public float TurnAngleLimit => _turnAngleLimit;
        public float TurnSpeed => _turnSpeed;
    }
}
