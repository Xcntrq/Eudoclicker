using nsMob;
using nsMobListItem;
using System.Collections.Generic;
using UnityEngine;

namespace nsMobList
{
    [CreateAssetMenu(menuName = "ScObs/MobList")]
    public class MobList : ScriptableObject
    {
        [SerializeField] private List<MobListItem> _items;

        public List<MobListItem> Items => _items;
    }
}
