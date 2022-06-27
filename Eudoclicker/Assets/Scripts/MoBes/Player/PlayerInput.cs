using nsMob;
using UnityEngine;
using UnityEngine.EventSystems;

namespace nsPlayerInput
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !IsOverGameObject())
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100.0f))
                {
                    Mob mob = hit.collider.GetComponent<Mob>();
                    if (mob != null) mob.Kill();

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
