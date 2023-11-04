using System;
using System.Collections.Generic;
using System.Linq;
using UniTimeReward.Domain;
using UniTimeReward.Domain.Model;

namespace UniTimeReward.Application.RewardFinder
{
    public class IndexedRewardFinder<TReward> : IRewardFinder<TReward> where TReward : IReward
    {
        private readonly IReadOnlyRepository<TReward> _claimed;
        private readonly IModifiedRepository<TReward> _modified;
        private readonly int _index; // Change to provider? unknown

        public IndexedRewardFinder(IReadOnlyRepository<TReward> claimed,
            IModifiedRepository<TReward> modified, int index)
        {
            _claimed = claimed;
            _modified = modified;
            _index = index;
        }
        
        public TReward Find()
        {
            var rewards = _claimed.GetRewards();
            var claimed = _modified.GetRewards();

            if (rewards.Count() - 1 < _index)
                throw new KeyNotFoundException();

            return rewards[_index];
        }

        public bool TryFind(out TReward reward)
        {
            reward = default(TReward);

            try
            {
                reward = Find();
            }
            catch (KeyNotFoundException)
            {
                return false;
            }

            return true;
        }
    }
}