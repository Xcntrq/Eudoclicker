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

        public event Action<string> OnValueChange;

        private void Awake()
        {
            _value = 0;
            _mobSpawner.OnMobCreate += MobSpawner_OnMobCreate;
        }

        private void Start()
        {
            OnValueChange?.Invoke(_value.ToString());
        }

        private void MobSpawner_OnMobCreate(Mob mob)
        {
            mob.OnDeath += Mob_OnDeath;
        }

        private void Mob_OnDeath(Mob mob)
        {
            _value++;
            OnValueChange?.Invoke(_value.ToString());
        }
    }
}
