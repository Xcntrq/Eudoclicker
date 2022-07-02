using nsIBooster;
using TMPro;
using UnityEngine;

namespace nsBoosterCDText
{
    public class BoosterCDText : MonoBehaviour
    {
        private TextMeshProUGUI _textMeshPro;

        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
            gameObject.SetActive(false);
        }

        public void Initialize(IBooster booster)
        {
            booster.OnHighestCooldownCheck += Booster_OnHighestCooldownCheck;
        }

        private void Booster_OnHighestCooldownCheck(float cooldown)
        {
            if (gameObject.activeInHierarchy)
            {
                if (cooldown > 0f)
                {
                    _textMeshPro.SetText(cooldown.ToString("0.0"));
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                if (cooldown > 0f)
                {
                    _textMeshPro.SetText(cooldown.ToString("0.0"));
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
