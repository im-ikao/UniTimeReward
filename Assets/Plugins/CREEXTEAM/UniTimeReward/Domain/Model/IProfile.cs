using NodaTime;

namespace UniTimeReward.Domain.Model
{
    public interface IProfile
    {
        public Instant? FirstRewardTime { get; }
        public Instant? LatestRewardTime { get; }
        public Duration AfterDuration => new Interval(LatestRewardTime, SystemClock.Instance.GetCurrentInstant()).Duration;

        public void SetFirstRewardTime(Instant firstReward);
        public void SetLatestRewardTime(Instant latestReward);
        public void Reset();
    }
}