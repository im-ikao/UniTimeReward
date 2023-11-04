using System;
using System.Collections.Generic;
using System.Data;
using UniTimeReward.Domain;
using UniTimeReward.Domain.Model;
using UniTimeReward.Infrastructure.Model;

namespace UniTimeReward.Infrastructure
{
    public class MockModifiedRepository : IModifiedRepository<BaseReward>
    {
        public Action<BaseReward> OnAdded { get; set; }
        public Action OnReset { get; set; }
        
        private readonly List<BaseReward> _rewards = new List<BaseReward>();
        
        public List<BaseReward> GetRewards()
        {
            return _rewards;
        }
        
        public void Add(BaseReward reward)
        {
            if (IsExists(reward) == true)
                throw new DuplicateNameException();
            
            _rewards.Add(reward);
            OnAdded?.Invoke(reward);
        }
        
        public bool TryAdd(BaseReward reward)
        {
            try
            {
                Add(reward);
            }
            catch (DuplicateNameException exception)
            {
                return false;
            }

            return true;
        }

        public void Reset()
        {
            _rewards.Clear();
            OnReset?.Invoke();
        }

        public bool IsExists(BaseReward reward)
        {
            return _rewards.Contains(reward);
        }

        public void Remove(BaseReward reward)
        {
            _rewards.Remove(reward);
        }
    }
}