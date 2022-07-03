using System;

namespace nsIOrientation2Provider
{
    public enum Orientation2
    {
        Unknown,
        Portrait,
        Landscape
    }

    public interface IOrientation2Provider
    {
        public event Action<Orientation2> OnOrientation2Change;
    }
}
