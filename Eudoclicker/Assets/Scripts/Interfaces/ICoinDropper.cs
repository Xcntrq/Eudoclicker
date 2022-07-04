using nsOnCoinDropEventArgs;
using System;

namespace nsICoinDropper
{
    public interface ICoinDropper
    {
        public event Action<OnCoinDropEventArgs> OnCoinDrop;
    }
}
