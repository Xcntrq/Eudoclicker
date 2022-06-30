using nsBoundValue;
using nsIKillable;
using nsILevelable;
using nsIntValue;
using nsMob;
using nsMobList;
using nsMobListItem;
using nsMobSpawnerData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace nsMobSpawner
{
    public enum SpawnerState
    {
        Cooldown,
        Spawning,
        Stopped
    }

    public class MobSpawner : MonoBehaviour
    {
        [SerializeField] private MobList _mobList;
        [SerializeField] private BoundsValue _spawnBounds;
        [SerializeField] private MobSpawnerData _mobSpawnerData;
        [SerializeField] private IntValue _seed;

        private HashSet<Mob> _spawnedMobs;
        private System.Random _random;
        private SpawnerState _currentSpawnerState;
        private float _timeLeft;
        private float _timeAdded;
        private float _minCooldown;
        private float _maxCooldown;
        private int _waveNumber;
        private string _spawnTimerText;

        //The one and only well encapsulated property!
        public IReadOnlyCollection<IKillable> KillableMobs
        {
            get
            {
                HashSet<IKillable> killables = new HashSet<IKillable>();
                foreach (Mob mob in _spawnedMobs)
                {
                    if (mob is IKillable killable) killables.Add(killable);
                }
                return killables;
            }
        }

        public event Action<Mob> OnMobCreate;
        public event Action<string> OnMobCountChange;
        public event Action<string> OnSpawnTimerChange;
        public event Action OnGameOverMobCountReach;

        private void Awake()
        {
            _timeLeft = 0f;
            _timeAdded = 0f;
            _waveNumber = 0;
            _minCooldown = _mobSpawnerData.MinCooldown;
            _maxCooldown = _mobSpawnerData.MaxCooldown;
            _spawnedMobs = new HashSet<Mob>();

            int randomInt = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
            _random = _seed.Value == 0 ? new System.Random(randomInt) : new System.Random(_seed.Value);

            _currentSpawnerState = SpawnerState.Spawning;
        }

        private void Start()
        {
            OnMobCountChange?.Invoke(string.Concat(_spawnedMobs.Count, '/', _mobSpawnerData.GameOverMobCount));
            OnSpawnTimerChange?.Invoke(string.Empty);
        }

        private void Update()
        {
            switch (_currentSpawnerState)
            {
                case SpawnerState.Cooldown:
                    _timeLeft -= Time.deltaTime;
                    if (_timeLeft <= 0)
                    {
                        _timeLeft = 0;
                        _currentSpawnerState = SpawnerState.Spawning;
                    }
                    _spawnTimerText = string.Concat("Spawn: ", _timeLeft.ToString("0.0"), " s<br>(+", _timeAdded.ToString("0.0"), " s)");
                    OnSpawnTimerChange?.Invoke(_spawnTimerText);
                    break;
                case SpawnerState.Spawning:
                    Mob mob = SpawnRandomMob();
                    _waveNumber++;
                    if (mob is ILevelable levelable) levelable.SetLevel(_waveNumber);
                    if (mob is IKillable killable) killable.OnDeath += Killable_OnDeath;
                    OnMobCreate?.Invoke(mob);
                    _spawnedMobs.Add(mob);
                    OnMobCountChange?.Invoke(string.Concat(_spawnedMobs.Count, '/', _mobSpawnerData.GameOverMobCount));
                    if (_spawnedMobs.Count < _mobSpawnerData.GameOverMobCount)
                    {
                        float lerpValue = (float)_random.NextDouble();
                        _timeAdded = Mathf.Lerp(_minCooldown, _maxCooldown, lerpValue);
                        _minCooldown *= 1f - _mobSpawnerData.DeltaCooldown;
                        _maxCooldown *= 1f - _mobSpawnerData.DeltaCooldown;
                        _timeLeft += _timeAdded;
                        _currentSpawnerState = SpawnerState.Cooldown;
                    }
                    else
                    {
                        OnGameOverMobCountReach?.Invoke();
                        _currentSpawnerState = SpawnerState.Stopped;
                    }
                    break;
                case SpawnerState.Stopped:
                    break;
            }
        }

        private Mob SpawnRandomMob()
        {
            Mob mobToSpawn = null;
            float totalWeight = 0;
            foreach (MobListItem mobListItem in _mobList.Items)
            {
                totalWeight += mobListItem.Weight;
            }
            float lerpValue = (float)_random.NextDouble();
            float weight = Mathf.Lerp(0f, totalWeight, lerpValue);
            foreach (MobListItem mobListItem in _mobList.Items)
            {
                weight -= mobListItem.Weight;
                if (weight <= 0f)
                {
                    mobToSpawn = mobListItem.Mob;
                    break;
                }
            }
            Vector3 spawnPosition = RandomPointWithinBound(_spawnBounds.Value);
            return Instantiate(mobToSpawn, spawnPosition, Quaternion.identity, transform);
        }

        private Vector3 RandomPointWithinBound(Bounds bounds)
        {
            float lerpValue = (float)_random.NextDouble();
            float spawnX = Mathf.Lerp(bounds.min.x, bounds.max.x, lerpValue);
            lerpValue = (float)_random.NextDouble();
            float spawnZ = Mathf.Lerp(bounds.min.z, bounds.max.z, lerpValue);
            return new Vector3(spawnX, 0, spawnZ);
        }

        private void Killable_OnDeath(IKillable killable)
        {
            if ((killable is Mob mob) && _spawnedMobs.Contains(mob))
            {
                _spawnedMobs.Remove(mob);
                OnMobCountChange?.Invoke(string.Concat(_spawnedMobs.Count, '/', _mobSpawnerData.GameOverMobCount));
            }
            if ((_spawnedMobs.Count == 0) && (_currentSpawnerState != SpawnerState.Stopped))
            {
                _currentSpawnerState = SpawnerState.Spawning;
            }
        }

        public void AddSeconds(int amount)
        {
            _timeAdded = amount;
            _timeLeft += _timeAdded;
        }
    }
}
