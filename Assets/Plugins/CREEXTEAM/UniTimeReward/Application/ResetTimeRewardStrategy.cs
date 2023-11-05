using NodaTime;
using UniTimeReward.Domain;
using UniTimeReward.Domain.Model;

namespace UniTimeReward.Application
{
    public class ResetTimeRewardStrategy<TReward> : BaseTimeRewardStrategy<TReward> where TReward : IReward
    {
        public ResetTimeRewardStrategy(
            IProfile profile,
            Duration duration,
            IRewardFinder<TReward> finder,
            IReadOnlyRepository<TReward> rewards,
            IModifiedRepository<TReward> unlocked,
            IModifiedRepository<TReward> claimed) : base(profile, duration, finder, rewards, unlocked, claimed)
        {
            
        }

        private protected override bool LastUpdate()
        {
            if (_profile.AfterDuration <= _duration.Plus(_duration)) 
                return true;
            
            Reset();
            
            return false;
        }
    }
}