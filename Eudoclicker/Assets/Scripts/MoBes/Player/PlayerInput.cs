using nsIDamageable;
using nsIDamage;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using nsVolumedClip;

namespace nsPlayerInput
{
    public class OnPewPewEventArgs : EventArgs
    {
        public VolumedClip VolumedClip { get; private set; }

        public OnPewPewEventArgs(VolumedClip volumedClip)
        {
            VolumedClip = volumedClip;
        }
    }

    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private VolumedClip _pewPewClip;

        private IDamage _damage;
        private Camera _camera;
        private OnPewPewEventArgs _onPewPewEventArgs;

        public event Action<OnPewPewEventArgs> OnPewPew;

        private void Awake()
        {
            _onPewPewEventArgs = new OnPewPewEventArgs(_pewPewClip);
            _damage = GetComponent<IDamage>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !IsOverGameObject())
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100.0f))
                {
                    IDamageable damagable = hit.collider.GetComponent<IDamageable>();
                    if (damagable == null) OnPewPew?.Invoke(_onPewPewEventArgs);
                    if (damagable != null) damagable.DecreaceHealth(_damage.Value);

                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1f);
                }
            }
        }

        private bool IsOverGameObject()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return true;

            foreach (Touch touch in Input.touches)
            {
                bool isTouchOverGameObject = (touch.phase == TouchPhase.Began) && EventSystem.current.IsPointerOverGameObject(touch.fingerId);
                if (isTouchOverGameObject) return true;
            }

            return false;
        }
    }
}
