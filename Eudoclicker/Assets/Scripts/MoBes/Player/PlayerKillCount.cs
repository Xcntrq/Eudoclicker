using nsIPointsGiver;
using nsMob;
using nsMobSpawner;
using nsOnPointGiveEventArgs;
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
            if (mob is IPointsGiver pointsGiver) pointsGiver.OnPointsGive += PointsGiver_OnPointsGive;
        }

        private void PointsGiver_OnPointsGive(OnPointsGiveEventArgs onDeathEventArgs)
        {
            _value += onDeathEventArgs.Amount;
            OnValueChange?.Invoke(_value);
        }
    }
}
