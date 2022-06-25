using nsICoinDropper;
using nsMob;
using nsMobSpawner;
using System;
using UnityEngine;

namespace nsPlayerCoinCount
{
    public class PlayerCoinCount : MonoBehaviour
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
            if (mob is ICoinDropper coinDropper) coinDropper.OnCoinDrop += CoinDropper_OnCoinDrop;
        }

        private void CoinDropper_OnCoinDrop()
        {
            _value++;
            OnValueChange?.Invoke(_value.ToString());
        }
    }
}
