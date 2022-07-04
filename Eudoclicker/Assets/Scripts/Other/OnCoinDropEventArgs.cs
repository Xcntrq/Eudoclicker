using nsVolumedClip;
using System;

namespace nsOnCoinDropEventArgs
{
    public class OnCoinDropEventArgs : EventArgs
    {
        public int Amount { get; private set; }
        public VolumedClip VolumedClip { get; private set; }

        public OnCoinDropEventArgs(int amount, VolumedClip volumedClip)
        {
            Amount = amount;
            VolumedClip = volumedClip;
        }
    }
}
