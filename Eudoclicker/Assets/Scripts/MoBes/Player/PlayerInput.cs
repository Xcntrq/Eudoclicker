using nsIDamagable;
using UnityEngine;
using UnityEngine.EventSystems;

namespace nsPlayerInput
{
    public class PlayerInput : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !IsOverGameObject())
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100.0f))
                {
                    IDamagable damagable = hit.collider.GetComponent<IDamagable>();
                    if (damagable != null) damagable.DecreaceHealth(1);

                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1f);
                }
            }
        }

        public bool IsOverGameObject()
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
