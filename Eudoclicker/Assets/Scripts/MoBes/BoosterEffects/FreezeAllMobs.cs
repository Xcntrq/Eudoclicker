using nsBoosterEffect;
using nsIFreezable;
using nsIKillable;
using nsMobSpawner;
using System.Collections.Generic;
using UnityEngine;

namespace nsFreezeAllMobs
{
    public class FreezeAllMobs : BoosterEffect
    {
        [SerializeField] private MobSpawner _mobSpawner;
        [SerializeField] private float _time;

        private HashSet<IFreezable> _freezables;
        private bool _areFrozen;
        private float _timeLeft;

        private void Awake()
        {
            _freezables = new HashSet<IFreezable>();
            _areFrozen = false;
        }

        private void Update()
        {
            if (!_areFrozen) return;

            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0f)
            {
                _areFrozen = false;
                foreach (IFreezable freezable in _freezables)
                {
                    freezable.Unfreeze();
                }
            }
        }

        public override void Proc(out string message)
        {
            message = string.Concat("All mobs frozen for ", _time, " sec");
            IReadOnlyCollection<IFreezable> freezableMobs = _mobSpawner.FreezableMobs;
            foreach (IFreezable freezable in freezableMobs)
            {
                if (!_freezables.Contains(freezable) && (freezable is IKillable killable)) killable.OnDeath += Killable_OnDeath;
            }
            _freezables.UnionWith(freezableMobs);
            foreach (IFreezable freezable in _freezables)
            {
                freezable.Freeze();
            }
            _areFrozen = true;
            _timeLeft = _time;
        }

        private void Killable_OnDeath(IKillable killable)
        {
            if ((killable is IFreezable freezable) && _freezables.Contains(freezable)) _freezables.Remove(freezable);
        }
    }
}
