using UnityEngine;
using UnityEngine.UI;

namespace nsSetActiveButton
{
    public class SetActiveButton : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private bool _value;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => _gameObject.SetActive(_value));
        }
    }
}
