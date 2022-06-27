using nsGameStateSwitch;
using nsHighscoreItem;
using nsHighscores;
using nsPlayerKillCount;
using nsPlayerName;
using UnityEngine;

namespace nsPlayerHighscore
{
    public class PlayerHighscore : MonoBehaviour
    {
        [SerializeField] private GameStateSwitch _gameStateSwitch;
        [SerializeField] private PlayerKillCount _playerKillCount;
        [SerializeField] private PlayerName _playerName;

        private Highscores _highscores;
        private HighscoreItem _playerHighscoreItem;

        private void Awake()
        {
            _highscores = new Highscores(false);
            _playerHighscoreItem = new HighscoreItem();
            _gameStateSwitch.OnPause += GameStateSwitch_OnSwitch;
            _gameStateSwitch.OnGameOver += GameStateSwitch_OnSwitch;
            _playerKillCount.OnValueChange += PlayerKillCount_OnValueChange;
            _playerName.OnPlayerNameChange += PlayerName_OnPlayerNameChange;
        }

        private void PlayerName_OnPlayerNameChange(string name)
        {
            _playerHighscoreItem.Name = name;
            _highscores.SavePlayerHighscore(_playerHighscoreItem);
        }

        private void PlayerKillCount_OnValueChange(int score)
        {
            _playerHighscoreItem.Score = score;
            _highscores.SavePlayerHighscore(_playerHighscoreItem);
        }

        private void GameStateSwitch_OnSwitch()
        {
            _highscores.SavePlayerHighscore(_playerHighscoreItem);
        }

    }
}
