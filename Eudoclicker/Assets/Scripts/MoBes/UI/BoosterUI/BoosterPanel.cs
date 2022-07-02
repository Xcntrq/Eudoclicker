using nsBooster;
using nsBoosterPanelItem;
using nsBoosterList;
using System;
using UnityEngine;
using nsPageSelectionButton;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace nsBoosterPanel
{
    public class BoosterPanel : MonoBehaviour
    {
        private const int _perPage = 3;

        [SerializeField] private BoosterList _boosterList;
        [SerializeField] private BoosterPanelItem _pfBoosterPanelItem;
        [SerializeField] private PageSelectionButton _leftButton;
        [SerializeField] private PageSelectionButton _rightButton;

        private List<List<BoosterPanelItem>> _panelPages;
        private int _currentPage;

        public event Action<string> OnButtonClick;

        private void Awake()
        {
            _currentPage = 0;
            _panelPages = new List<List<BoosterPanelItem>>();
            _leftButton.GetComponent<Button>().onClick.AddListener(LeftButtonClick);
            _rightButton.GetComponent<Button>().onClick.AddListener(RightButtonClick);
        }

        private void Start()
        {
            for (int i = 0; i < _boosterList.Items.Length; i++)
            {
                if (i % _perPage == 0) _panelPages.Add(new List<BoosterPanelItem>());

                Booster booster = _boosterList.Items[i];
                BoosterPanelItem newBoosterPanelItem = Instantiate(_pfBoosterPanelItem, transform);

                _panelPages.Last().Add(newBoosterPanelItem);

                newBoosterPanelItem.gameObject.SetActive(false);
                newBoosterPanelItem.Initialize(booster);
                newBoosterPanelItem.Button.onClick.AddListener(
                    () =>
                    {
                        if (booster.AreAllConditionsSatisfied(out string allConditionMessages))
                        {
                            booster.ProcAllEffects(out string allEffectMessages);
                            string text = string.Concat(booster.BoosterData.Name, "<br><br>", allEffectMessages, "<br><br>", allConditionMessages);
                            OnButtonClick?.Invoke(text);
                        }
                        else
                        {
                            string text = string.Concat(booster.BoosterData.Name, "<br><br>", allConditionMessages);
                            OnButtonClick?.Invoke(text);
                        }
                    });
            }
            SwitchPage(0);
        }

        public void LeftButtonClick()
        {
            if (_currentPage > 0) SwitchPage(-1);
        }

        public void RightButtonClick()
        {
            if (_currentPage < _panelPages.Count - 1) SwitchPage(1);
        }

        private void SwitchPage(int delta)
        {
            SetCurrentPageItemsActive(false);
            _currentPage += delta;
            _leftButton.Switch(_currentPage != 0);
            _rightButton.Switch(_currentPage != _panelPages.Count - 1);
            SetCurrentPageItemsActive(true);
        }

        private void SetCurrentPageItemsActive(bool value)
        {
            foreach (BoosterPanelItem boosterPanelItem in _panelPages[_currentPage])
            {
                boosterPanelItem.gameObject.SetActive(value);
            }
        }
    }
}
