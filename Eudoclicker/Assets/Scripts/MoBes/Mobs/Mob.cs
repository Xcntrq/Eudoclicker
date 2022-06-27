using System;
using UnityEngine;

namespace nsMob
{
    public abstract class Mob : MonoBehaviour
    {
        protected int _waveNumber;

        public event Action<Mob> OnDeath;

        public void Initialize(int waveNumber)
        {
            _waveNumber = waveNumber;
            PostInitialize();
        }

        protected virtual void PostInitialize()
        {

        }

        public void Kill()
        {
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
