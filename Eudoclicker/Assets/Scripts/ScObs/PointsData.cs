using UnityEngine;

namespace nsPointsData
{
    [CreateAssetMenu(menuName = "ScObs/PointsData")]
    public class PointsData : ScriptableObject
    {
        [SerializeField] private int _amount;

        public int Amount => _amount;
    }
}
