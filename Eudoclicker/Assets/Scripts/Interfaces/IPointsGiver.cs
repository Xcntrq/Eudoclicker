using nsOnPointGiveEventArgs;
using System;

namespace nsIPointsGiver
{
    public interface IPointsGiver
    {
        public event Action<OnPointsGiveEventArgs> OnPointsGive;
    }
}
