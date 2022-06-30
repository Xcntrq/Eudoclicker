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

        public int Value => _value;

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

        private void CoinDropper_OnCoinDrop(int coinAmount)
        {
            _value += coinAmount;
            OnValueChange?.Invoke(_value.ToString());
        }

        public void ReduceCoinCount(int coinAmount)
        {
            _value -= coinAmount;
            OnValueChange?.Invoke(_value.ToString());
        }
    }
}
