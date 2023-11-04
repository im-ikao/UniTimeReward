using System;
using System.Linq;
using UniTimeReward.Domain;
using UniTimeReward.Domain.Model;

namespace UniTimeReward.Application.RewardFinder
{
    public class SkipLockedAfterLastUnlockedRewardFinder<TReward> : IRewardFinder<TReward> where TReward : IReward
    {
        private readonly IReadOnlyRepository<TReward> _claimed;
        private readonly IModifiedRepository<TReward> _modified;
        private readonly int _skip; // Change to skip provider? unknown or Settings

        public SkipLockedAfterLastUnlockedRewardFinder(IReadOnlyRepository<TReward> claimed,
            IModifiedRepository<TReward> modified, int skip)
        {
            _claimed = claimed;
            _modified = modified;
            _skip = skip;
        }
        
        public TReward Find()
        {
            var rewards = _claimed.GetRewards();
            var claimed = _modified.GetRewards();
            var lastClaimed = rewards.Last(x => claimed.Contains(x));
            var indexOfClaimed = rewards.IndexOf(lastClaimed);
            var founded = rewards.Skip(indexOfClaimed + _skip).ToArray();

            if (founded.Any() == false)
                throw new OverflowException();

            return founded.First();
        }
        
        public bool TryFind(out TReward reward)
        {
            reward = default(TReward);

            try
            {
                reward = Find();
            }
            catch (OverflowException)
            {
                return false;
            }

            return true;
        }
    }
}