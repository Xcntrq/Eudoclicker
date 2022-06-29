using UnityEngine;

namespace nsCoinDropperData
{
    [CreateAssetMenu(menuName = "ScObs/CoinDropperData")]
    public class CoinDropperData : ScriptableObject
    {
        [SerializeField] private float _coinChance;

        public float CoinChance => _coinChance;
    }
}
