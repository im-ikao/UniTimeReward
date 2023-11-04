using System;
using UniTimeReward.Domain.Model;

namespace UniTimeReward.Domain
{
    public interface IModifiedRepository<TReward> : IReadOnlyRepository<TReward> where TReward : IReward
    {
        public Action<TReward> OnAdded { get; set; }
        public Action OnReset { get; set; }
        
        public void Add(TReward reward);
        public bool TryAdd(TReward reward);
        public void Reset();
        public bool IsExists(TReward reward);
        public void Remove(TReward reward);
    }
}