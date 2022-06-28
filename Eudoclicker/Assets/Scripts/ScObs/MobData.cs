using UnityEngine;

namespace nsMobData
{
    [CreateAssetMenu(menuName = "ScObs/MobData")]
    public class MobData : ScriptableObject
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private float _waveNumberHealthBoost;

        public int MaxHealth => _maxHealth;
        public float WaveNumberHealthBoost => _waveNumberHealthBoost;
    }
}
