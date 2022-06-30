using nsFloatValue;
using nsIMovementSpeedChanger;
using UnityEngine;

namespace nsAnimatorSpeed
{
    public class AnimatorSpeed : MonoBehaviour
    {
        [SerializeField] private FloatValue _multiplier;

        private IMovementSpeedChanger _speedChanger;
        private Animator _animator;

        private void Awake()
        {
            _speedChanger = GetComponentInParent<IMovementSpeedChanger>();
            _speedChanger.OnSpeedChange += SpeedChanger_OnSpeedChange;
            _animator = GetComponent<Animator>();
        }

        private void SpeedChanger_OnSpeedChange(float speed)
        {
            _animator.speed = speed * _multiplier.Value;
        }
    }
}
