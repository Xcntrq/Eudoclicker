using nsBoosterCDText;
using nsBoosterIconFiller;
using nsBoosterReadyDot;
using nsIBooster;
using UnityEngine;
using UnityEngine.UI;

namespace nsBoosterPanelItem
{
    public class BoosterPanelItem : MonoBehaviour
    {
        [SerializeField] private Image _buttonImage;

        private BoosterIconFiller _boosterIconFiller;
        private BoosterReadyDot _boosterReadyDot;
        private BoosterCDText _boosterCDText;

        public Button Button { get; private set; }

        private void Awake()
        {
            _boosterIconFiller = GetComponentInChildren<BoosterIconFiller>();
            _boosterReadyDot = GetComponentInChildren<BoosterReadyDot>();
            _boosterCDText = GetComponentInChildren<BoosterCDText>();
            Button = GetComponentInChildren<Button>();
        }

        public void Initialize(IBooster booster)
        {
            if (_buttonImage != null) _buttonImage.sprite = booster.ButtonSprite;
            if (_boosterIconFiller != null) _boosterIconFiller.Initialize(booster);
            if (_boosterReadyDot != null) _boosterReadyDot.Initialize(booster);
            if (_boosterCDText != null) _boosterCDText.Initialize(booster);
        }
    }
}
