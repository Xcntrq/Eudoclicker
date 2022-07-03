using nsGameStateSwitch;
using UnityEngine;
using UnityEngine.UI;

namespace nsPauseButton
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private GameStateSwitch _gameStateSwitch;

        private void Awake()
        {
            Button button = GetComponentInChildren<Button>();
            button.onClick.AddListener(() => _gameStateSwitch.TogglePause());
        }
    }
}
