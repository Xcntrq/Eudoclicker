using nsMob;
using UnityEngine;

namespace nsPlayerInput
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100.0f))
                {
                    Mob mob = hit.collider.GetComponent<Mob>();
                    if (mob != null) Destroy(mob.gameObject);

                    Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1f);
                }
            }
        }
    }
}
