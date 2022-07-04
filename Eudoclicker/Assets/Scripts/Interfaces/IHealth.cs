using nsOnHealthDecreaceEventArgs;

namespace nsIHealth
{
    public interface IHealth
    {
        public int Value { get; }

        public OnHealthDecreaceEventArgs Decrease(int amount, bool isSoundNeeded);
    }
}
