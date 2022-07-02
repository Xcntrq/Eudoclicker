using nsIDamageable;
using nsIDamage;
using UnityEngine;
using UnityEngine.EventSystems;

namespace nsPlayerInput
{
    public class PlayerInput : MonoBehaviour
    {
        private IDamage _damage;
        private Camera _camera;

        private void Awake()
        {
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
