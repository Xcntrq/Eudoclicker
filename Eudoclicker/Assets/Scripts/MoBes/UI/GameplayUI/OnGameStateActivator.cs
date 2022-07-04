using nsGameStateSwitch;
using nsOnOrientation2Activator;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace nsOnGameStateActivator
{
    [Serializable]
    public struct StateAction
    {
        [SerializeField] private GameState _gameState;
        [SerializeField] private bool _isActive;

        public GameState GameState => _gameState;
        public bool IsActive => _isActive;
    }

    public class OnGameStateActivator : MonoBehaviour
    {
        [SerializeField] private GameStateSwitch _gameStateSwitch;
        [SerializeField] private List<StateAction> _items;

        private OnOrientation2Activator _onOrientationActivator;

        public bool IsActive { get; private set; }

        private void Awake()
        {
            IsActive = true; //In case OnOrientationActivator is present on the gameObject
            _onOrientationActivator = GetComponent<OnOrientation2Activator>();
            _gameStateSwitch.OnGameStateChange += GameStateSwitch_OnGameStateChange;
        }

        private void GameStateSwitch_OnGameStateChange(GameState gameState)
        {
            foreach (StateAction stateAction in _items)
            {
                if (stateAction.GameState == gameState)
                {
                    bool isOnOrientationActivatorActive = (_onOrientationActivator == null) || _onOrientationActivator.IsActive;
                    IsActive = stateAction.IsActive;
                    gameObject.SetActive(IsActive && isOnOrientationActivatorActive);
                }
            }
        }
    }
}
