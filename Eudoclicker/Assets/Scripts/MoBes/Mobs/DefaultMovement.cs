using UniRandom = UnityEngine.Random;
using SysRandom = System.Random;
using nsBoundValue;
using nsDefaultMovementData;
using nsIntValue;
using UnityEngine;
using nsIMobBehaviour;
using System;
using nsIMovementSpeedChanger;
using nsILevelable;

namespace nsDefaultMovement
{
    public class DefaultMovement : MonoBehaviour, IMobBehaviour, IMovementSpeedChanger, ILevelable
    {
        [SerializeField] private DefaultMovementData _defaultMovementData;
        [SerializeField] private BoundsValue _movementBounds;
        [SerializeField] private IntValue _behaviourSeed;

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

        public event Action<float> OnSpeedChange;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            int randomInt = UniRandom.Range(int.MinValue, int.MaxValue);
            _behaviourRandom = _behaviourSeed.Value == 0 ? new SysRandom(randomInt) : new SysRandom(_behaviourSeed.Value);

            transform.forward = RandomDirection(180f);
            _targetDirection = transform.forward; //Needed in DrawRaysForFun
            _isTargetRotationReached = true;
        }

        public void SetLevel(int level)
        {
            _speed = _defaultMovementData.Speed * (1f + level * _defaultMovementData.LevelBonus);
            OnSpeedChange?.Invoke(_speed);
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
            if ((_defaultMovementData.TurnProbability > 0f) && (_turnDecider <= _defaultMovementData.TurnProbability))
            {
                _targetDirection = RandomDirection(_defaultMovementData.TurnAngleLimit);
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
                _rotationLerp += Time.deltaTime * _defaultMovementData.TurnSpeed;
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
