namespace Funda.Service
{
    public class Settings
    {
        public Settings(int maxPropertyCount)
        {
            this.MaxPropertyCount = maxPropertyCount;
        }
        public int ItemCountInBillBoard = 10;
        public int MaxPageSize = 25;
        public readonly int MaxPropertyCount; //Can be get by Funda service.
        public int RetryInMiliSeconds = 5000;
    }
}