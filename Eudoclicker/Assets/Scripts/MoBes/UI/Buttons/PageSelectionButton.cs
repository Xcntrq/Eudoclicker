using UnityEngine;
using UnityEngine.UI;

namespace nsPageSelectionButton
{
    public class PageSelectionButton : MonoBehaviour
    {
        [SerializeField] private Sprite _spriteOn;
        [SerializeField] private Sprite _spriteOff;

        private Button _button;
        private Image _mainImage;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _mainImage = GetComponentInChildren<Image>();
        }

        public void Switch(bool on)
        {
            _button.interactable = on;
            _mainImage.sprite = on ? _spriteOn : _spriteOff;
        }
    }
}
