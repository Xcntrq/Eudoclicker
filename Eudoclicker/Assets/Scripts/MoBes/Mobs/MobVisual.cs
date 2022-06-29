using nsISpawnFinishWaiter;
using nsMob;
using UnityEngine;

namespace nsMobVisual
{
    public class MobVisual : MonoBehaviour
    {
        [SerializeField] private Mob _mob;

        public void FinishSpawn()
        {
            if (_mob is ISpawnFinishWaiter spawnFinishWaiter) spawnFinishWaiter.OnSpawnFinish();
        }
    }
}
