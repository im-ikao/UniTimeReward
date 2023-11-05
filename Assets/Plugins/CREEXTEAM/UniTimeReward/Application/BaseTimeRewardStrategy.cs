using System;
using System.Linq;
using NodaTime;
using UniTimeReward.Domain;
using UniTimeReward.Domain.Model;
using UniTimeReward.Infrastructure;
using UniTimeReward.Infrastructure.Model;

namespace UniTimeReward.Application
{
    public class BaseTimeRewardStrategy<TReward> : IRewardStrategy where TReward : IReward
    {
        private protected readonly IProfile _profile;
        private protected readonly Duration _duration;

        private readonly IRewardFinder<TReward> _finder;
        private readonly IReadOnlyRepository<TReward> _rewards;
        private readonly IModifiedRepository<TReward> _unlocked;
        private readonly IModifiedRepository<TReward> _claimed;
        
        public BaseTimeRewardStrategy(
            IProfile profile,
            Duration duration,
            IRewardFinder<TReward> finder,
            IReadOnlyRepository<TReward> rewards,
            IModifiedRepository<TReward> unlocked,
            IModifiedRepository<TReward> claimed)
        {
            _profile = profile;
            _duration = duration;
            _finder = finder;
            _rewards = rewards;
            _unlocked = unlocked;
            _claimed = claimed;
        }

        public void Claim()
        {
            var unlockedRewards = _unlocked.GetRewards();
            
            if (unlockedRewards.Any() == false)
                return;

            for (var index = 0; index < unlockedRewards.Count; index++)
            {
                var unlocked = unlockedRewards[index];
                _claimed.TryAdd(unlocked);
                _unlocked.Remove(unlocked);
            }

            var currentTime = SystemClock.Instance.GetCurrentInstant();
            
            if (_profile.FirstRewardTime == null)
                _profile.SetFirstRewardTime(currentTime);
            
            _profile.SetLatestRewardTime(currentTime);
        }

        public void Update()
        {
            if (_profile.LatestRewardTime != null)
            {
                if (_profile.AfterDuration < _duration)
                    return;
            }
            
            if (LastUpdate() == false)
                return;

            if (_finder.TryFind(out var unlock) == false)
                return;
            
            TryOpen(unlock);
        }

        private protected virtual bool LastUpdate()
        {
            return true;
        }

        public void Reset()
        {
            _claimed.Reset();
            _unlocked.Reset();
            _profile.Reset();
        }
        
        private bool TryOpen(TReward reward)
        {
            return _unlocked.TryAdd(reward);
        }
    }
}