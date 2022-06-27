using nsLastUsedName;
using System;
using UnityEngine;

namespace nsPlayerName
{
    public class PlayerName : MonoBehaviour
    {
        private LastUsedName _lastUsedName;
        private string _value;

        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
                _lastUsedName.Value = _value;
                OnPlayerNameChange?.Invoke(_value);
            }
        }

        public event Action<string> OnPlayerNameChange;

        private void Awake()
        {
            _lastUsedName = new LastUsedName();
        }

        private void Start()
        {
            Value = _lastUsedName.Value;
        }
    }
}
