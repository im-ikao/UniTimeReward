namespace UniTimeReward.Domain
{
    public interface IRewardStrategy
    {
        public void Claim();
        public void Update();
        public void Reset();
    }
}