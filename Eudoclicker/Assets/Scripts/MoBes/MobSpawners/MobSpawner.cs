using nsBoundValue;
using nsIMobForgetter;
using nsIntValue;
using nsMob;
using nsMobList;
using nsMobSpawnerData;
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

    public class MobSpawner : MonoBehaviour, IMobForgetter
    {
        [SerializeField] private MobList _mobList;
        [SerializeField] private BoundsValue _spawnBounds;
        [SerializeField] private MobSpawnerData _mobSpawnerData;
        [SerializeField] private IntValue _seed;

        private HashSet<Mob> _spawnedMobs;
        private System.Random _random;
        private SpawnerState _spawnerState;
        private float _timeLeft;
        private float _minCooldown;
        private float _maxCooldown;
        private int _waveNumber;

        private void Awake()
        {
            _waveNumber = 0;
            _minCooldown = _mobSpawnerData.MinCooldown;
            _maxCooldown = _mobSpawnerData.MaxCooldown;
            _spawnedMobs = new HashSet<Mob>();

            int randomInt = Random.Range(int.MinValue, int.MaxValue);
            _random = _seed.Value == 0 ? new System.Random(randomInt) : new System.Random(_seed.Value);

            _spawnerState = SpawnerState.Spawning;
        }

        private void Update()
        {
            switch (_spawnerState)
            {
                case SpawnerState.Cooldown:
                    _timeLeft -= Time.deltaTime;
                    if (_timeLeft <= 0)
                    {
                        _spawnerState = SpawnerState.Spawning;
                    }
                    break;
                case SpawnerState.Spawning:
                    Vector3 spawnPosition = RandomPointWithinBound(_spawnBounds.Value);
                    int index = _random.Next(_mobList.Items.Count);
                    Mob mob = Instantiate(_mobList.Items[index], spawnPosition, Quaternion.identity, transform);
                    mob.Initialize(this, _waveNumber++);
                    _spawnedMobs.Add(mob);
                    if (_spawnedMobs.Count < _mobSpawnerData.MaxMobAmount)
                    {
                        float lerpValue = (float)_random.NextDouble();
                        _timeLeft = Mathf.Lerp(_minCooldown, _maxCooldown, lerpValue);
                        _minCooldown *= 1f - _mobSpawnerData.DeltaCooldown;
                        _maxCooldown *= 1f - _mobSpawnerData.DeltaCooldown;
                        _spawnerState = SpawnerState.Cooldown;
                    }
                    else
                    {
                        _spawnerState = SpawnerState.Stopped;
                    }
                    break;
                case SpawnerState.Stopped:
                    break;
            }
        }

        private Vector3 RandomPointWithinBound(Bounds bounds)
        {
            float lerpValue = (float)_random.NextDouble();
            float spawnX = Mathf.Lerp(bounds.min.x, bounds.max.x, lerpValue);
            lerpValue = (float)_random.NextDouble();
            float spawnZ = Mathf.Lerp(bounds.min.z, bounds.max.z, lerpValue);
            return new Vector3(spawnX, 0, spawnZ);
        }

        public void ForgetMob(Mob mob)
        {
            if (_spawnedMobs.Contains(mob)) _spawnedMobs.Remove(mob);
        }
    }
}
