using UnityEngine;

namespace nsBoosterIconFiller
{
    public class BoosterIconFiller : MonoBehaviour
    {
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void ChangeScale(float scale)
        {
            Vector3 newScale = _rectTransform.localScale;
            newScale.y = 1f - scale;
            _rectTransform.localScale = newScale;
        }
    }
}
