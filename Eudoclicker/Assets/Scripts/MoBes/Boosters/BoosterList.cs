using nsBooster;
using UnityEngine;

namespace nsBoosterList
{
    public class BoosterList : MonoBehaviour
    {
        [SerializeField] private Booster[] _items;

        public Booster[] Items => _items;


    }
}
