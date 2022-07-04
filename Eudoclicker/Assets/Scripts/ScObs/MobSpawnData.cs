using nsVolumedClip;
using UnityEngine;

namespace nsMobSpawnData
{
    [CreateAssetMenu(menuName = "ScObs/MobSpawnData")]
    public class MobSpawnData : ScriptableObject
    {
        [SerializeField] private VolumedClip _spawnClip;

        public VolumedClip SpawnClip => _spawnClip;
    }
}