using nsICoinDropper;
using nsMob;
using nsMobSpawner;
using nsOnCoinDropEventArgs;
using System;
using UnityEngine;

namespace nsPlayerCoinCount
{
    public class PlayerCoinCount : MonoBehaviour
    {
        [SerializeField] private MobSpawner _mobSpawner;
        [SerializeField] private int _startAmount;

        private int _value;

        public int Value => _value;

        public event Action<int> OnValueChange;

        private void Awake()
        {
            _value = _startAmount;
            _mobSpawner.OnMobCreate += MobSpawner_OnMobCreate;
        }

        private void Start()
        {
            OnValueChange?.Invoke(_value);
        }

        private void MobSpawner_OnMobCreate(Mob mob)
        {
            if (mob is ICoinDropper coinDropper) coinDropper.OnCoinDrop += CoinDropper_OnCoinDrop;
        }

        private void CoinDropper_OnCoinDrop(OnCoinDropEventArgs onCoinDropEventArgs)
        {
            _value += onCoinDropEventArgs.Amount;
            OnValueChange?.Invoke(_value);
        }

        public void ReduceCoinCount(int coinAmount)
        {
            _value -= coinAmount;
            OnValueChange?.Invoke(_value);
        }

        public void IncreaceCoinCount(int coinAmount)
        {
            _value += coinAmount;
            OnValueChange?.Invoke(_value);
        }
    }
}
