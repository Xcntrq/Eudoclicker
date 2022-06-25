using nsMob;
using System.Collections.Generic;
using UnityEngine;

namespace nsMobList
{
    [CreateAssetMenu(menuName = "ScObs/MobList")]
    public class MobList : ScriptableObject
    {
        [SerializeField] private List<Mob> _items;

        public List<Mob> Items => _items;
    }
}
