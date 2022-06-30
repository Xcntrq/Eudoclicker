using UnityEngine;

namespace nsCoinsData
{
    [CreateAssetMenu(menuName = "ScObs/CoinsData")]
    public class CoinsData : ScriptableObject
    {
        [Header("e.g., 2.4 gives 2 and a 40% chance of 1 more")]
        [SerializeField] private float _coinChance;

        public float CoinChance => _coinChance;
    }
}
