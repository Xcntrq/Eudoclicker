using System;

namespace nsICoinDropper
{
    public interface ICoinDropper
    {
        public event Action<int> OnCoinDrop;
    }
}
