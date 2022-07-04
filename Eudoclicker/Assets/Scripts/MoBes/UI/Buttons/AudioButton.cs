using nsMusicAndSounds;
using UnityEngine;
using UnityEngine.UI;

namespace nsAudioButton
{
    public abstract class AudioButton : MonoBehaviour
    {
        [SerializeField] protected MusicAndSounds _musicAndSounds;

        [SerializeField] private Image _border;
        [SerializeField] private Sprite _spriteWhenOn;
        [SerializeField] private Sprite _spriteWhenOff;
        [SerializeField] private Color _colorWhenOn;
        [SerializeField] private Color _colorWhenOff;

        private Image _image = null;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        protected abstract void OnClick();

        public void SetImage(bool isOn)
        {
            if (_image == null) _image = GetComponent<Image>();
            if (!_image || !_spriteWhenOn || !_spriteWhenOff) return;
            _image.sprite = isOn ? _spriteWhenOn : _spriteWhenOff;
            _image.color = isOn ? _colorWhenOn : _colorWhenOff;
            _border.color = isOn ? _colorWhenOn : _colorWhenOff;
        }
    }
}
