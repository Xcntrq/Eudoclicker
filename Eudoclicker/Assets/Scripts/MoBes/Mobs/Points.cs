using nsIPoints;
using nsOnPointGiveEventArgs;
using nsPointsData;
using UnityEngine;

namespace nsPoints
{
    public class Points : MonoBehaviour, IPoints
    {
        [SerializeField] private PointsData _pointsData;

        public OnPointsGiveEventArgs Give()
        {
            return new OnPointsGiveEventArgs(_pointsData.Amount);
        }
    }
}
