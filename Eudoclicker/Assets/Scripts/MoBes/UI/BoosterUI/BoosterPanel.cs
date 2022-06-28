using nsBooster;
using nsBoosterPanelItem;
using nsBoosterList;
using System;
using UnityEngine;

namespace nsBoosterPanel
{
    public class BoosterPanel : MonoBehaviour
    {
        [SerializeField] private BoosterList _boosterList;
        [SerializeField] private BoosterPanelItem _pfBoosterPanelItem;

        public event Action<string> OnButtonClick;

        private void Awake()
        {
            foreach (Booster _booster in _boosterList.Items)
            {
                BoosterPanelItem newBoosterPanelItem = Instantiate(_pfBoosterPanelItem, transform);
                newBoosterPanelItem.Initialize(_booster);
                newBoosterPanelItem.Button.onClick.AddListener(
                    () =>
                    {
                        if (_booster.AreAllConditionsSatisfied(out string allConditionMessages))
                        {
                            _booster.ProcAllEffects(out string allEffectMessages);
                            string text = string.Concat(_booster.BoosterData.Name, "<br><br>", allEffectMessages, "<br><br>", allConditionMessages);
                            OnButtonClick?.Invoke(text);
                        }
                        else
                        {
                            string text = string.Concat(_booster.BoosterData.Name, "<br><br>", allConditionMessages);
                            OnButtonClick?.Invoke(text);
                        }
                    });
            }
        }
    }
}
