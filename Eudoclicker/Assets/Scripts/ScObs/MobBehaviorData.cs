using UnityEngine;

namespace nsMobBehaviorData
{
    [CreateAssetMenu(menuName = "ScObs/MobBehaviorData")]
    public class MobBehaviorData : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _waveNumberSpeedBoost;
        [SerializeField] private float _turnProbability;
        [SerializeField] private float _turnAngleLimit;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private float _coinChance;

        public float Speed => _speed;
        public float WaveNumberSpeedBoost => _waveNumberSpeedBoost;
        public float TurnProbability => _turnProbability;
        public float TurnAngleLimit => _turnAngleLimit;
        public float TurnSpeed => _turnSpeed;
        public float CoinChance => _coinChance;
    }
}
