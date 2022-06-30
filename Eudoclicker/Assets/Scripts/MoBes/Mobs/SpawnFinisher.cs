using nsISpawnFinisher;
using System;
using UnityEngine;

namespace nsSpawnFinisher
{
    public class SpawnFinisher : MonoBehaviour, ISpawnFinisher
    {
        public event Action OnSpawnFinish;

        public void FinishSpawn()
        {
            OnSpawnFinish();
        }
    }
}
