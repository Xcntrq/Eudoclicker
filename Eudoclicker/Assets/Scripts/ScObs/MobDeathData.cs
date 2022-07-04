using nsVolumedClip;
using UnityEngine;

namespace nsMobDeathData
{
    [CreateAssetMenu(menuName = "ScObs/MobDeathData")]
    public class MobDeathData : ScriptableObject
    {
        [SerializeField] private VolumedClip _deathClip;

        public VolumedClip DeathClip => _deathClip;
    }
}
