using nsVolumedClip;
using UnityEngine;

namespace nsHealthData
{
    [CreateAssetMenu(menuName = "ScObs/HealthData")]
    public class HealthData : ScriptableObject
    {
        [Header("e.g., 0.2 gives 20% per level")]
        [SerializeField] private float _levelBonus;
        [SerializeField] private int _maxHealth;
        [SerializeField] private VolumedClip _hurtClip;

        public float LevelBonus => _levelBonus;
        public int MaxHealth => _maxHealth;
        public VolumedClip HurtClip => _hurtClip;
    }
}
