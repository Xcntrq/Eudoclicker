using nsBoundValue;
using nsIntValue;
using nsMob;
using nsMobDataDefaultCube;
using UnityEngine;

namespace nsMobDefaultCube
{
    public class DefaultMob : Mob
    {
        [SerializeField] private DefaultMobData _mobDataDefaultCube;
        [SerializeField] private BoundsValue _movementBounds;
        [SerializeField] private IntValue _behaviorSeed;

        private Rigidbody _rigidbody;

        private System.Random _behaviorRandom;
        private Vector3 _cachedDirection;
        private Vector3 _currentDirection;
        private Vector3 _targetDirection;
        private float _rotationLerp;
        private float _speed;
        private bool _isTargetRotationReached;

        private float _turnDecider;
        private float _angleDeviationLerp;
        private float _angleDeviation;
        private bool _isOutOfBounds;
        private bool _isOutOnX;
        private bool _isOutOnZ;
        private Vector3 _directionMirrored;

        protected override void PostInitialize()
        {
            _speed = _mobDataDefaultCube.Speed * (1f + _waveNumber * _mobDataDefaultCube.WaveNumberMultiplier);
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            int randomInt = Random.Range(int.MinValue, int.MaxValue);
            _behaviorRandom = _behaviorSeed.Value == 0 ? new System.Random(randomInt) : new System.Random(_behaviorSeed.Value);

            _speed = _mobDataDefaultCube.Speed;
            _targetDirection = transform.forward; //Needed in DrawRaysForFun
            _isTargetRotationReached = true;
        }

        private void FixedUpdate()
        {
            //When out of bounds just mirror the direction and stop turning
            _isOutOfBounds = _movementBounds.Value.Contains(transform.position) == false;
            if (_isOutOfBounds)
            {
                _isOutOnX = (transform.position.x < _movementBounds.Value.min.x) || (transform.position.x > _movementBounds.Value.max.x);
                _isOutOnZ = (transform.position.z < _movementBounds.Value.min.z) || (transform.position.z > _movementBounds.Value.max.z);

                _directionMirrored = transform.forward;
                if (_isOutOnX) _directionMirrored.x *= -1f;
                if (_isOutOnZ) _directionMirrored.z *= -1f;

                transform.forward = _directionMirrored;
                transform.position = _movementBounds.Value.ClosestPoint(transform.position);
                _isTargetRotationReached = true;
            }

            //Roll the dice and turn if procs
            _turnDecider = 1f - (float)_behaviorRandom.NextDouble();
            if ((_mobDataDefaultCube.TurnProbability > 0f) && (_turnDecider <= _mobDataDefaultCube.TurnProbability))
            {
                _angleDeviationLerp = (float)_behaviorRandom.NextDouble();
                _angleDeviation = Mathf.Lerp(-_mobDataDefaultCube.TurnAngleLimit, _mobDataDefaultCube.TurnAngleLimit, _angleDeviationLerp);
                _targetDirection = Quaternion.AngleAxis(_angleDeviation, Vector3.up) * transform.forward;
                _cachedDirection = transform.forward;
                _isTargetRotationReached = false;
                _rotationLerp = 0f;
            }

            _rigidbody.velocity = transform.forward * _speed;
        }

        private void Update()
        {
            if (_isTargetRotationReached == false)
            {
                _rotationLerp += Time.deltaTime * _mobDataDefaultCube.TurnSpeed;
                _currentDirection = Vector3.Lerp(_cachedDirection, _targetDirection, _rotationLerp);
                transform.forward = _currentDirection;
                if (_rotationLerp > 1f) _isTargetRotationReached = true;
            }

            DrawRaysForFun();
        }

        private void DrawRaysForFun()
        {
            Vector3 start = transform.position + transform.up * 0.1f;
            Debug.DrawRay(start, transform.forward * 2f, Color.blue, 0f);
            Debug.DrawRay(start, _targetDirection * 2f, Color.black, 0f);
        }
    }
}