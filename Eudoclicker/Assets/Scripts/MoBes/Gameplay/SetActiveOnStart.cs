using UnityEngine;

namespace nsSetActiveOnStart
{
    public class SetActiveOnStart : MonoBehaviour
    {
        [SerializeField] private bool _value;

        private void Start()
        {
            gameObject.SetActive(_value);
        }
    }
}
