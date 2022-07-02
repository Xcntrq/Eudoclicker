using nsBooster;
using UnityEngine;

namespace nsBoosterList
{
    public class BoosterList : MonoBehaviour
    {
        [SerializeField] private Booster[] _items;

        public Booster[] Items => _items;

        private void Awake()
        {
            if (_items.Length == 0) _items = GetComponentsInChildren<Booster>();
        }

        //For the editor
        public void PopulateArrays()
        {
            _items = GetComponentsInChildren<Booster>();
        }
    }
}
