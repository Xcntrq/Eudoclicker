using nsGameStateSwitch;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace nsActiveOnGameStates
{
    [Serializable]
    public struct StateAction
    {
        [SerializeField] private GameState _gameState;
        [SerializeField] private bool _isActive;

        public GameState GameState => _gameState;
        public bool IsActive => _isActive;
    }

    public class ActiveOnGameStates : MonoBehaviour
    {
        [SerializeField] private GameStateSwitch _gameStateSwitch;
        [SerializeField] private List<StateAction> _items;

        private void Awake()
        {
            _gameStateSwitch.OnGameStateChange += GameStateSwitch_OnGameStateChange;
        }

        private void GameStateSwitch_OnGameStateChange(GameState gameState)
        {
            foreach (StateAction stateAction in _items)
            {
                if (stateAction.GameState == gameState) gameObject.SetActive(stateAction.IsActive);
            }
        }
    }
}
