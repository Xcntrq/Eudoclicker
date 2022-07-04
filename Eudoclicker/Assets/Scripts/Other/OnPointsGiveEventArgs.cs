namespace nsOnPointGiveEventArgs
{
    public class OnPointsGiveEventArgs
    {
        public int Amount { get; private set; }

        public OnPointsGiveEventArgs(int amount)
        {
            Amount = amount;
        }
    }
}
