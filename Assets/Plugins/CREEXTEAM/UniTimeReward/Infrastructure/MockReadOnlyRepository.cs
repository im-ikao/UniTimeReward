using System;
using System.Collections.Generic;
using UniTimeReward.Domain;
using UniTimeReward.Infrastructure.Model;

namespace UniTimeReward.Infrastructure
{
    public class MockReadOnlyRepository : IReadOnlyRepository<BaseReward>
    {
        private readonly List<BaseReward> _rewards = new List<BaseReward>()
        {
            new()
            {
                Id = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid()
            }
        };
        
        public List<BaseReward> GetRewards()
        {
            return _rewards;
        }
    }
}