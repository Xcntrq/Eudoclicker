using UniRandom = UnityEngine.Random;
using SysRandom = System.Random;
using nsBoundValue;
using nsDefaultMovementData;
using nsIntValue;
using nsIWaveNumberHolder;
using UnityEngine;
using nsIMobBehaviour;
using System;
using nsISpeedCarrier;

namespace nsDefaultMovement
{
    public class DefaultMovement : MonoBehaviour, IMobBehaviour, ISpeedCarrier
    {
        [SerializeField] private DefaultMovementData _defaultBehaviourData;
        [SerializeField] private BoundsValue _movementBounds;
        [SerializeField] private IntValue _behaviourSeed;

        private IWaveNumberCarrier _waveNumberHolder;
        private SysRandom _behaviourRandom;
        private Rigidbody _rigidbody;

        private Vector3 _cachedDirection;
        private Vector3 _currentDirection;
        private Vector3 _targetDirection;

        private float _rotationLerp;
        private float _speed;
        private float _turnDecider;
        private bool _isOutOfBounds;
        private bool _isTargetRotationReached;

        private float Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                OnSpeedChange?.Invoke(_speed);
            }
        }

        public event Action<float> OnSpeedChange;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _waveNumberHolder = GetComponent<IWaveNumberCarrier>();

            int randomInt = UniRandom.Range(int.MinValue, int.MaxValue);
            _behaviourRandom = _behaviourSeed.Value == 0 ? new SysRandom(randomInt) : new SysRandom(_behaviourSeed.Value);

            transform.forward = RandomDirection(180f);
            Speed = _defaultBehaviourData.Speed;
            _targetDirection = transform.forward; //Needed in DrawRaysForFun
            _isTargetRotationReached = true;
        }

        private void OnEnable()
        {
            //Mobs should enable all of their IMobBehaviours after getting a WaveNumber
            Speed = _defaultBehaviourData.Speed * (1f + _waveNumberHolder.WaveNumber * _defaultBehaviourData.WaveNumberSpeedBoost);
        }

        private void FixedUpdate()
        {
            //When out of bounds just mirror the direction and stop turning
            _isOutOfBounds = _movementBounds.Value.Contains(transform.position) == false;
            if (_isOutOfBounds)
            {
                bool isOutOnX = (transform.position.x < _movementBounds.Value.min.x) || (transform.position.x > _movementBounds.Value.max.x);
                bool isOutOnZ = (transform.position.z < _movementBounds.Value.min.z) || (transform.position.z > _movementBounds.Value.max.z);

                Vector3 directionMirrored = transform.forward;
                if (isOutOnX) directionMirrored.x *= -1f;
                if (isOutOnZ) directionMirrored.z *= -1f;

                transform.forward = directionMirrored;
                transform.position = _movementBounds.Value.ClosestPoint(transform.position);
                _isTargetRotationReached = true;
            }

            //Roll the dice and turn if procs
            _turnDecider = 1f - (float)_behaviourRandom.NextDouble();
            if ((_defaultBehaviourData.TurnProbability > 0f) && (_turnDecider <= _defaultBehaviourData.TurnProbability))
            {
                _targetDirection = RandomDirection(_defaultBehaviourData.TurnAngleLimit);
                _cachedDirection = transform.forward;
                _isTargetRotationReached = false;
                _rotationLerp = 0f;
            }

            _rigidbody.velocity = transform.forward * Speed;
        }

        private void Update()
        {
            if (_isTargetRotationReached == false)
            {
                _rotationLerp += Time.deltaTime * _defaultBehaviourData.TurnSpeed;
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

        private Vector3 RandomDirection(float angleLimit)
        {
            float angleDeviationLerp = (float)_behaviourRandom.NextDouble();
            float angleDeviation = Mathf.Lerp(-angleLimit, angleLimit, angleDeviationLerp);
            return Quaternion.AngleAxis(angleDeviation, Vector3.up) * transform.forward;
        }

        public void SetComponentActive(bool value)
        {
            enabled = value;
        }
    }
}
