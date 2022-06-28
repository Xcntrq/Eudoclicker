using nsColorValue;
using nsHealth;
using UnityEngine;
using UnityEngine.UI;

namespace nsHealthBar
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private RectTransform _valueBar;
        [SerializeField] private Image _image;
        [SerializeField] private ColorValue _highColor;
        [SerializeField] private ColorValue _mediumColor;
        [SerializeField] private ColorValue _lowColor;

        private Camera _camera;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Initialize(Health health, Camera camera)
        {
            _camera = camera;
            health.OnValueChange += Health_OnValueChange;
        }

        private void Health_OnValueChange(float valueNormalized)
        {
            if (valueNormalized > 0.99f)
            {
                gameObject.SetActive(false);
                return;
            }
            else if (valueNormalized > 0.66f)
            {
                _image.color = _highColor.Value;
            }
            else if (valueNormalized > 0.33f)
            {
                _image.color = _mediumColor.Value;
            }
            else
            {
                _image.color = _lowColor.Value;
            }

            Vector3 newLocalScale = _valueBar.localScale;
            newLocalScale.x = valueNormalized;
            _valueBar.localScale = newLocalScale;
            gameObject.SetActive(true);
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + _camera.transform.forward);
        }
    }
}
