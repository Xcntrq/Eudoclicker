using nsIMobForgetter;
using UnityEngine;

namespace nsMob
{
    public abstract class Mob : MonoBehaviour
    {
        private IMobForgetter _mobSpawner;

        protected int _waveNumber;

        public void Initialize(IMobForgetter mobSpawner, int waveNumber)
        {
            _mobSpawner = mobSpawner;
            _waveNumber = waveNumber;
            PostInitialize();
        }

        protected virtual void PostInitialize()
        {

        }

        protected virtual void OnDestroy()
        {
            if (_mobSpawner != null) _mobSpawner.ForgetMob(this);
        }
    }
}
