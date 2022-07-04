using UnityEngine;

namespace nsMobSpawnerData
{
    [CreateAssetMenu(menuName = "ScObs/MobSpawnerData")]
    public class MobSpawnerData : ScriptableObject
    {
        [SerializeField] private int _gameOverMobCount;
        [SerializeField] private float _minCooldown;
        [SerializeField] private float _maxCooldown;
        [SerializeField] private float _deltaCooldown;
        [SerializeField] private float _delayWhenZero;

        public int GameOverMobCount => _gameOverMobCount;
        public float MinCooldown => _minCooldown;
        public float MaxCooldown => _maxCooldown;
        public float DeltaCooldown => _deltaCooldown;
        public float DelayWhenZero => _delayWhenZero;
    }
}
