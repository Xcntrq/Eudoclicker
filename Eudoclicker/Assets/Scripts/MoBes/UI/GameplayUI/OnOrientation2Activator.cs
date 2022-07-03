using nsIOrientation2Provider;
using nsOnGameStateActivator;
using nsOrientationProvider;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace nsOnOrientation2Activator
{
    [Serializable]
    public struct Orientation2Action
    {
        [SerializeField] private Orientation2 _orientation2;
        [SerializeField] private bool _isActive;

        public Orientation2 Orientation2 => _orientation2;
        public bool IsActive => _isActive;
    }

    public class OnOrientation2Activator : MonoBehaviour
    {
        [SerializeField] private List<Orientation2Action> _items;

        private OnGameStateActivator _onGameStateActivator;
        private IOrientation2Provider _orientation2Provider;

        public bool IsActive { get; private set; }

        private void Awake()
        {
            IsActive = true; //In case OnGameStateActivator is present on the gameObject
            _orientation2Provider = FindObjectOfType<OrientationProvider>(false);
            _orientation2Provider.OnOrientation2Change += Orientation2Provider_OnOrientation2Change;
            _onGameStateActivator = GetComponent<OnGameStateActivator>();
        }

        private void Orientation2Provider_OnOrientation2Change(Orientation2 deviceOrientation2)
        {
            foreach (Orientation2Action orientation2Action in _items)
            {
                if (orientation2Action.Orientation2 == deviceOrientation2)
                {
                    bool isOnGameStateActivatorActive = (_onGameStateActivator == null) || _onGameStateActivator.IsActive;
                    IsActive = orientation2Action.IsActive;
                    gameObject.SetActive(IsActive && isOnGameStateActivatorActive);
                }
            }
        }
    }
}
