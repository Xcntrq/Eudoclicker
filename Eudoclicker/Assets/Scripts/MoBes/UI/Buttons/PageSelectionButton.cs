using nsIPageNumber;
using UnityEngine;
using UnityEngine.UI;

namespace nsPageSelectionButton
{
    public abstract class PageSelectionButton : MonoBehaviour
    {
        [SerializeField] private Sprite _spriteOn;
        [SerializeField] private Sprite _spriteOff;

        private Button _button;
        private Image _mainImage;

        protected IPageNumber _pageNumber;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
            _mainImage = GetComponentInChildren<Image>();
            _pageNumber = GetComponentInParent<IPageNumber>();
            _pageNumber.OnValueChange += PageNumber_OnValueChange;
        }

        protected abstract void PageNumber_OnValueChange(int value);

        protected abstract void OnClick();

        protected void Switch(bool on)
        {
            _button.interactable = on;
            _mainImage.sprite = on ? _spriteOn : _spriteOff;
        }
    }
}
