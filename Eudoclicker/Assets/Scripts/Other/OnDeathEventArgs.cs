using nsIKillable;
using nsVolumedClip;
using System;

namespace nsOnDeathEventArgs
{
    public class OnDeathEventArgs : EventArgs
    {
        public IKillable Killable { get; private set; }
        public VolumedClip VolumedClip { get; private set; }

        public OnDeathEventArgs(IKillable killable, VolumedClip volumedClip)
        {
            Killable = killable;
            VolumedClip = volumedClip;
        }
    }
}
