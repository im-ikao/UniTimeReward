using System.Collections.Generic;
using UniTimeReward.Domain.Model;

namespace UniTimeReward.Domain
{
    public interface IReadOnlyRepository<TReward> where TReward : IReward
    {
        public List<TReward> GetRewards(); // TODO: Dictionary
    }
}