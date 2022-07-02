using nsIBooster;
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

        public void Initialize(IBooster booster)
        {
            booster.OnReadyPercentagesCheck += Booster_OnReadyPercentagesCheck;
        }

        public void Booster_OnReadyPercentagesCheck(float scale)
        {
            Vector3 newScale = _rectTransform.localScale;
            newScale.y = 1f - scale;
            _rectTransform.localScale = newScale;
        }
    }
}
