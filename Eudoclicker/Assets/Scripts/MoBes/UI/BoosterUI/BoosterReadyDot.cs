using nsIBooster;
using UnityEngine;

namespace nsBoosterReadyDot
{
    public class BoosterReadyDot : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Initialize(IBooster booster)
        {
            booster.OnReadyPercentagesCheck += Booster_OnReadyPercentagesCheck;
        }

        private void Booster_OnReadyPercentagesCheck(float value)
        {
            if (gameObject.activeInHierarchy)
            {
                if (value < 1f)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                if (value == 1f)
                {
                    gameObject.SetActive(true);
                }
            }
        }
    }
}
