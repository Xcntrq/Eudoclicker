using System;
using UnityEngine;

namespace nsCoinValueProvider
{
    public abstract class CoinValueProvider : MonoBehaviour
    {
        public abstract event Action<int> OnValueChange;
    }
}
