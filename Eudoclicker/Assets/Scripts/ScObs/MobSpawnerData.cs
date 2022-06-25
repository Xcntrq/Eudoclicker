using UnityEngine;

namespace nsMobSpawnerData
{
    [CreateAssetMenu(menuName = "ScObs/MobSpawnerData")]
    public class MobSpawnerData : ScriptableObject
    {
        [SerializeField] private int _maxMobAmount;
        [SerializeField] private float _minCooldown;
        [SerializeField] private float _maxCooldown;
        [SerializeField] private float _deltaCooldown;

        public int MaxMobAmount => _maxMobAmount;
        public float MinCooldown => _minCooldown;
        public float MaxCooldown => _maxCooldown;
        public float DeltaCooldown => _deltaCooldown;
    }
}
