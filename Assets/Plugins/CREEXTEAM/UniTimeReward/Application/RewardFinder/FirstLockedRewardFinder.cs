using System;
using System.Linq;
using JetBrains.Annotations;
using UniTimeReward.Domain;
using UniTimeReward.Domain.Model;

namespace UniTimeReward.Application.RewardFinder
{
    public class FirstLockedRewardFinder<TReward> : IRewardFinder<TReward> where TReward : IReward
    {
        private readonly IReadOnlyRepository<TReward> _claimed;
        private readonly IModifiedRepository<TReward> _modified;

        public FirstLockedRewardFinder(IReadOnlyRepository<TReward> claimed, IModifiedRepository<TReward> modified)
        {
            _claimed = claimed;
            _modified = modified;
        }
        
        public TReward Find()
        {
            var rewards = _claimed.GetRewards();
            var claimed = _modified.GetRewards();
            var exceptClaimed = rewards
                .Except(claimed)
                .ToArray();

            if (exceptClaimed.Any() == false)
                throw new OverflowException();

            return exceptClaimed.First();
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