using nsBooster;
using nsBoosterPanelItem;
using nsBoosterList;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using nsIPageNumber;
using nsVolumedClip;

namespace nsBoosterPanel
{
    public class OnButtonClickEventArgs : EventArgs
    {
        public string Text { get; private set; }
        public VolumedClip VolumedClip { get; private set; }

        public OnButtonClickEventArgs(string text, VolumedClip volumedClip)
        {
            Text = text;
            VolumedClip = volumedClip;
        }
    }

    public class BoosterPanel : MonoBehaviour
    {
        private const int _perPage = 3;

        [SerializeField] private BoosterList _boosterList;
        [SerializeField] private BoosterPanelItem _pfBoosterPanelItem;
        [SerializeField] private VolumedClip _notAvailableClip;

        private List<List<BoosterPanelItem>> _panelPages;
        private IPageNumber _pageNumber;
        private int _currentPage;

        public event Action<OnButtonClickEventArgs> OnButtonClick;

        private void Awake()
        {
            _currentPage = 0;
            _panelPages = null;
            _pageNumber = GetComponentInParent<IPageNumber>();
            _pageNumber.OnValueChange += PageNumber_OnValueChange;
        }

        private void Start()
        {
            if (_panelPages == null)
            {
                FillPages();
                _pageNumber.Init(0, _currentPage, _panelPages.Count - 1);
            }
        }

        private void FillPages()
        {
            _panelPages = new List<List<BoosterPanelItem>>();
            for (int i = 0; i < _boosterList.Items.Length; i++)
            {
                if (i % _perPage == 0) _panelPages.Add(new List<BoosterPanelItem>());

                Booster booster = _boosterList.Items[i];
                BoosterPanelItem newBoosterPanelItem = Instantiate(_pfBoosterPanelItem, transform);

                _panelPages.Last().Add(newBoosterPanelItem);

                newBoosterPanelItem.Initialize(booster);
                newBoosterPanelItem.gameObject.SetActive(false);
                newBoosterPanelItem.Button.onClick.AddListener(
                    () =>
                    {
                        if (booster.AreAllConditionsSatisfied(out string allConditionMessages))
                        {
                            booster.ProcAllEffects(out string allEffectMessages);
                            string text = string.Concat(booster.BoosterData.Name, "<br><br>", allEffectMessages, "<br><br>", allConditionMessages);
                            OnButtonClickEventArgs onButtonClickEventArgs = new OnButtonClickEventArgs(text, booster.BoosterData.SuccessClip);
                            OnButtonClick?.Invoke(onButtonClickEventArgs);
                        }
                        else
                        {
                            string text = string.Concat(booster.BoosterData.Name, "<br><br>", allConditionMessages);
                            OnButtonClickEventArgs onButtonClickEventArgs = new OnButtonClickEventArgs(text, _notAvailableClip);
                            OnButtonClick?.Invoke(onButtonClickEventArgs);
                        }
                    });
            }
        }

        private void PageNumber_OnValueChange(int value)
        {
            if (_panelPages != null)
            {
                SetPageActive(_currentPage, false);
                SetPageActive(value, true);
            }
            _currentPage = value;
        }

        private void SetPageActive(int i, bool on)
        {
            foreach (BoosterPanelItem boosterPanelItem in _panelPages[i])
            {
                boosterPanelItem.gameObject.SetActive(on);
            }
        }
    }
}
