using nsFloatValue;
using nsISpeedProvider;
using nsMob;
using UnityEngine;

namespace nsAnimatorSpeed
{
    public class AnimatorSpeed : MonoBehaviour
    {
        [SerializeField] private Mob _mob;
        [SerializeField] private FloatValue _multiplier;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            if (_mob is ISpeedProvider speedProvider) speedProvider.OnSpeedChange += SpeedProvider_OnSpeedChange;
        }

        private void SpeedProvider_OnSpeedChange(float speed)
        {
            _animator.speed = speed * _multiplier.Value;
        }
    }
}
