using System;
using UnityEngine;

namespace nsIBooster
{
    public interface IBooster
    {
        public Sprite ButtonSprite { get; }

        public event Action<float> OnReadyPercentagesCheck;
        public event Action<float> OnHighestCooldownCheck;
    }
}
