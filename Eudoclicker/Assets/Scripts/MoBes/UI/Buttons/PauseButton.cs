using nsGameStateSwitch;
using UnityEngine;
using UnityEngine.UI;

namespace nsPauseButton
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private GameStateSwitch _gameStateSwitch;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => _gameStateSwitch.TogglePause());
        }
    }
}
