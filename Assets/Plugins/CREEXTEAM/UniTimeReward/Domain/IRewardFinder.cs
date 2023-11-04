using UniTimeReward.Domain.Model;

namespace UniTimeReward.Domain
{
    public interface IRewardFinder<TReward> where TReward : IReward
    {
        public TReward Find();
        public bool TryFind(out TReward reward);
    }
}