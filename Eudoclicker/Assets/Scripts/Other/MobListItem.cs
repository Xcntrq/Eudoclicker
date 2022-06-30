using nsMob;
using System;
using UnityEngine;

namespace nsMobListItem
{
    [Serializable]
    public class MobListItem
    {
        [SerializeField] private Mob _mob;
        [SerializeField] private float _weight;

        public Mob Mob => _mob;
        public float Weight => _weight;
    }
}
