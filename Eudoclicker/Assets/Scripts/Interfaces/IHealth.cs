namespace nsIHealth
{
    public interface IHealth
    {
        public int Value { get; }

        public void Decrease(int amount);
    }
}
