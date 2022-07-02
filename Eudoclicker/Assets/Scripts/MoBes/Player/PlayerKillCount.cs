using nsIKillable;
using nsMob;
using nsMobSpawner;
using System;
using UnityEngine;

namespace nsPlayerKillCount
{
    public class PlayerKillCount : MonoBehaviour
    {
        [SerializeField] private MobSpawner _mobSpawner;

        private int _value;

        public int Value => _value;

        public event Action<int> OnValueChange;

        private void Awake()
        {
            _value = 0;
            _mobSpawner.OnMobCreate += MobSpawner_OnMobCreate;
        }

        private void Start()
        {
            OnValueChange?.Invoke(_value);
        }

        private void MobSpawner_OnMobCreate(Mob mob)
        {
            if (mob is IKillable killable) killable.OnDeath += Mob_OnDeath;
        }

        private void Mob_OnDeath(IKillable killable)
        {
            _value++;
            OnValueChange?.Invoke(_value);
        }
    }
}
