using System;
using NodaTime;
using UniTimeReward.Domain.Model;

namespace UniTimeReward.Infrastructure.Model
{
    [Serializable]
    public class BaseProfile : IProfile
    {
        public BaseProfile(Instant? firstReward, Instant? latestReward)
        {
            FirstRewardTime = firstReward;
            LatestRewardTime = latestReward;
        }
        
        public Instant? FirstRewardTime { get; private set; }
        public Instant? LatestRewardTime { get; private set; }
        
        public void SetFirstRewardTime(Instant firstReward)
        {
            FirstRewardTime = firstReward;
        }

        public void SetLatestRewardTime(Instant latestReward)
        {
            LatestRewardTime = latestReward;
        }

        public void Reset()
        {
            FirstRewardTime = null;
            LatestRewardTime = null;
        }
    }
}