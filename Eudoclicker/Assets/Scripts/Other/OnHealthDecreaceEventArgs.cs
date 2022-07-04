using nsVolumedClip;
using System;

namespace nsOnHealthDecreaceEventArgs
{
    public class OnHealthDecreaceEventArgs : EventArgs
    {
        public VolumedClip VolumedClip { get; private set; }

        public OnHealthDecreaceEventArgs(VolumedClip volumedClip)
        {
            VolumedClip = volumedClip;
        }
    }
}
