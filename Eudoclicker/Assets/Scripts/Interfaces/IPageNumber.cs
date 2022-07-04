using System;

namespace nsIPageNumber
{
    public interface IPageNumber
    {
        public int MinValue { get; }
        public int MaxValue { get; }

        public event Action<int> OnValueChange;

        public void Init(int minValue, int value, int maxValue);

        public void Decrease();

        public void Increase();
    }
}
