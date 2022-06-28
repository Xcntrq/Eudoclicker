using nsBooster;
using UnityEngine;

namespace nsBoosterIconFiller
{
    public class BoosterIconFiller : MonoBehaviour
    {
        private RectTransform _rectTransform;

        public void Initialize(Booster booster)
        {
            booster.OnReadyPercentagesCheck += Booster_OnReadyPercentagesCheck;
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Booster_OnReadyPercentagesCheck(float scale)
        {
            Vector3 newScale = _rectTransform.localScale;
            newScale.y = 1f - scale;
            _rectTransform.localScale = newScale;
        }
    }
}
