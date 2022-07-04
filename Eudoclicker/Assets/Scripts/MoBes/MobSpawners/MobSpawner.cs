using UniRandom = UnityEngine.Random;
using SysRandom = System.Random;
using nsBoundValue;
using nsIDamageable;
using nsIFreezable;
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
using System.Collections;
using nsOnDeathEventArgs;

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
        private SysRandom _random;
        private SpawnerState _currentSpawnerState;
        private float _timeLeft;
        private float _timeAdded;
        private float _minCooldown;
        private float _maxCooldown;
        private int _waveNumber;
        private int _gameOverMobCount;
        private string _spawnTimerText;

        public float TimeLeft => _timeLeft;

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

        public IReadOnlyCollection<IDamageable> DamageableMobs
        {
            get
            {
                HashSet<IDamageable> damageables = new HashSet<IDamageable>();
                foreach (Mob mob in _spawnedMobs)
                {
                    if (mob is IDamageable damageable) damageables.Add(damageable);
                }
                return damageables;
            }
        }

        public IReadOnlyCollection<IFreezable> FreezableMobs
        {
            get
            {
                HashSet<IFreezable> freezables = new HashSet<IFreezable>();
                foreach (Mob mob in _spawnedMobs)
                {
                    if (mob is IFreezable freezable) freezables.Add(freezable);
                }
                return freezables;
            }
        }

        public event Action<Mob> OnMobCreate;
        public event Action<int, int> OnMobCountChange;
        public event Action<string> OnSpawnTimerChange;
        public event Action OnGameOverMobCountReach;

        private void Awake()
        {
            _timeLeft = 0f;
            _timeAdded = 0f;
            _waveNumber = 0;
            _gameOverMobCount = _mobSpawnerData.GameOverMobCount;
            _minCooldown = _mobSpawnerData.MinCooldown;
            _maxCooldown = _mobSpawnerData.MaxCooldown;
            _spawnedMobs = new HashSet<Mob>();

            int randomInt = UniRandom.Range(int.MinValue, int.MaxValue);
            _random = _seed.Value == 0 ? new SysRandom(randomInt) : new SysRandom(_seed.Value);

            _currentSpawnerState = SpawnerState.Spawning;
        }

        private void Start()
        {
            OnMobCountChange?.Invoke(_spawnedMobs.Count, _gameOverMobCount);
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
                    OnMobCountChange?.Invoke(_spawnedMobs.Count, _gameOverMobCount);
                    if (_spawnedMobs.Count < _gameOverMobCount)
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

        private void Killable_OnDeath(OnDeathEventArgs onDeathEventArgs)
        {
            if ((onDeathEventArgs.Killable is Mob mob) && _spawnedMobs.Contains(mob))
            {
                _spawnedMobs.Remove(mob);
                OnMobCountChange?.Invoke(_spawnedMobs.Count, _gameOverMobCount);
            }
            if ((_spawnedMobs.Count == 0) && (_currentSpawnerState != SpawnerState.Stopped))
            {
                StartCoroutine(DelaySpawn());
            }
        }

        private IEnumerator DelaySpawn()
        {
            _currentSpawnerState = SpawnerState.Stopped;
            yield return new WaitForSeconds(_mobSpawnerData.DelayWhenZero);
            _currentSpawnerState = SpawnerState.Spawning;
        }

        public void AddTime(float amount)
        {
            _timeAdded = amount;
            _timeLeft += _timeAdded;
        }

        public void IncreaseGameOverMobCount(int amount)
        {
            _gameOverMobCount += amount;
            OnMobCountChange?.Invoke(_spawnedMobs.Count, _gameOverMobCount);
        }
    }
}
