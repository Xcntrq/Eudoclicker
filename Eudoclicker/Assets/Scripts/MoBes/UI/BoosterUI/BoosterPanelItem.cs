using nsBoosterIconFiller;
using UnityEngine;
using UnityEngine.UI;

namespace nsBoosterPanelItem
{
    public class BoosterPanelItem : MonoBehaviour
    {
        [SerializeField] private Image _mainImage;

        public Button Button { get; private set; }
        public BoosterIconFiller BoosterIconFiller { get; private set; }

        public void Initialize(Sprite mainSprite)
        {
            _mainImage.sprite = mainSprite;
        }

        private void Awake()
        {
            Button = GetComponentInChildren<Button>();
            BoosterIconFiller = GetComponentInChildren<BoosterIconFiller>();
        }
    }
}
