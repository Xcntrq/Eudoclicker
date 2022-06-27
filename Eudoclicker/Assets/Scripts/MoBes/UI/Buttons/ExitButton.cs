using UnityEngine;
using UnityEngine.UI;

namespace nsExitButton
{
    public class ExitButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            if (Application.platform == RuntimePlatform.Android) gameObject.SetActive(false);
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => Application.Quit());
        }
    }
}
