using System;
using UnityEngine;
using UnityEngine.UI;

namespace nsClearHighscoresButton
{
    public class ClearHighscoresButton : MonoBehaviour
    {
        private Button _button;

        public event Action OnClick;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => OnClick?.Invoke());
        }
    }
}
