using UniTimeReward.Domain.Model;

namespace UniTimeReward.Domain
{
    public interface IClaimStrategy<TReward> where TReward : IReward
    {
        public void Claim(TReward reward);
        public bool TryClaim(TReward reward);
        public bool CanClaim(TReward reward);
    }
}