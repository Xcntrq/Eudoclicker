using nsColorValue;
using nsIHealthValue;
using UnityEngine;
using UnityEngine.UI;

namespace nsHealthBar
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private RectTransform _valueBarRT;
        [SerializeField] private Image _valueBarImage;
        [SerializeField] private ColorValue _highColor;
        [SerializeField] private ColorValue _mediumColor;
        [SerializeField] private ColorValue _lowColor;

        private IHealthValue _healthValue;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            _healthValue = GetComponentInParent<IHealthValue>();
            _healthValue.OnValueChange += HealthValue_OnValueChange;
            gameObject.SetActive(false);
        }

        private void HealthValue_OnValueChange(float valueNormalized)
        {
            if (valueNormalized > 0.99f)
            {
                gameObject.SetActive(false);
                return;
            }
            else if (valueNormalized > 0.66f)
            {
                _valueBarImage.color = _highColor.Value;
            }
            else if (valueNormalized > 0.33f)
            {
                _valueBarImage.color = _mediumColor.Value;
            }
            else
            {
                _valueBarImage.color = _lowColor.Value;
            }

            Vector3 newLocalScale = _valueBarRT.localScale;
            newLocalScale.x = valueNormalized;
            _valueBarRT.localScale = newLocalScale;
            gameObject.SetActive(true);
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + _camera.transform.forward);
        }
    }
}
