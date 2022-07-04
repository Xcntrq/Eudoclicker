using System;

namespace nsIOrientation4Provider
{
    public enum Orientation4
    {
        Unknown,
        PortraitUp,
        PortraitDown,
        LandscapeLeft,
        LandscapeRight
    }

    public interface IOrientation4Provider
    {
        public event Action<Orientation4> OnOrientation4Change;
    }
}
