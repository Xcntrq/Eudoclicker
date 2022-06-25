using UnityEngine;

namespace nsMobData
{
    public abstract class MobData : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _waveNumberMultiplier;

        public float Speed => _speed;
        public float WaveNumberMultiplier => _waveNumberMultiplier;
    }
}
