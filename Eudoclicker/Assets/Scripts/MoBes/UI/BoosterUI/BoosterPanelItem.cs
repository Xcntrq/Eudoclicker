using nsBooster;
using nsBoosterIconFiller;
using UnityEngine;
using UnityEngine.UI;

namespace nsBoosterPanelItem
{
    public class BoosterPanelItem : MonoBehaviour
    {
        [SerializeField] private Image _mainImage;

        private BoosterIconFiller _boosterIconFiller;

        public Button Button { get; private set; }

        public void Initialize(Booster booster)
        {
            _mainImage.sprite = booster.BoosterData.ButtonSprite;
            _boosterIconFiller.Initialize(booster);
        }

        private void Awake()
        {
            Button = GetComponentInChildren<Button>();
            _boosterIconFiller = GetComponentInChildren<BoosterIconFiller>();
        }
    }
}
