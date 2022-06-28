using nsBoosterPanel;
using System.Collections;
using TMPro;
using UnityEngine;

namespace nsTextInTheCenter
{
    public class TextInTheCenter : MonoBehaviour
    {
        [SerializeField] private BoosterPanel _boosterPanel;
        [SerializeField] private float _messageDelay;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _boosterPanel.OnButtonClick += BoosterPanel_OnButtonClick;
        }

        private void BoosterPanel_OnButtonClick(string text)
        {
            StopAllCoroutines();
            _text.SetText(text);
            gameObject.SetActive(true);
            StartCoroutine(ShowMessage());
        }

        private IEnumerator ShowMessage()
        {
            var waitForSeconds = new WaitForSeconds(_messageDelay);
            yield return waitForSeconds;
            gameObject.SetActive(false);
        }
    }
}
