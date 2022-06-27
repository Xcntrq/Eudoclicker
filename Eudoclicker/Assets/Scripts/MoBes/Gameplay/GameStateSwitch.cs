using nsMobSpawner;
using System;
using UnityEngine;

namespace nsGameStateSwitch
{
    public enum GameState
    {
        Playing,
        Paused,
        Over
    }

    public class GameStateSwitch : MonoBehaviour
    {
        [SerializeField] private MobSpawner _mobSpawner;

        private GameState _currentGameState;

        public event Action OnPause;
        public event Action OnResume;
        public event Action OnGameOver;
        public event Action<GameState> OnGameStateChange;

        private void Awake()
        {
            _mobSpawner.OnGameOverMobCountReach += GameOver;
        }

        private void Start()
        {
            Resume();
        }

        public void TogglePause()
        {
            switch (_currentGameState)
            {
                case GameState.Playing:
                    Pause();
                    break;
                case GameState.Paused:
                    Resume();
                    break;
            }
        }

        private void Pause()
        {
            _currentGameState = GameState.Paused;
            OnGameStateChange?.Invoke(_currentGameState);
            OnPause?.Invoke();
            Time.timeScale = 0f;
        }

        private void Resume()
        {
            _currentGameState = GameState.Playing;
            OnGameStateChange?.Invoke(_currentGameState);
            OnResume?.Invoke();
            Time.timeScale = 1f;
        }

        private void GameOver()
        {
            _currentGameState = GameState.Over;
            OnGameStateChange?.Invoke(_currentGameState);
            OnGameOver?.Invoke();
            Time.timeScale = 1f;
        }
    }
}
