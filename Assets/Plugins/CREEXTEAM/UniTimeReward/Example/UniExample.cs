using System;
using System.Collections;
using System.Collections.Generic;
using NodaTime;
using UniTimeReward.Application;
using UniTimeReward.Application.RewardFinder;
using UniTimeReward.Domain;
using UniTimeReward.Domain.Model;
using UniTimeReward.Infrastructure;
using UniTimeReward.Infrastructure.Model;
using UnityEngine;

public class UniExample : MonoBehaviour
{
    private IProfile _profile;
    private IRewardStrategy _strategy;
    private IRewardFinder<BaseReward> _finder;
    
    private IReadOnlyRepository<BaseReward> _rewards;
    private IModifiedRepository<BaseReward> _unlocked;
    private IModifiedRepository<BaseReward> _claimed;
    
    private Duration _duration = Duration.FromSeconds(10);
    
    public void Awake()
    {
        _profile = new BaseProfile(null, null);
        _rewards = new MockReadOnlyRepository();
        _unlocked = new MockModifiedRepository();
        _claimed = new MockModifiedRepository();
        _finder = new FirstLockedRewardFinder<BaseReward>(_rewards, _claimed);
        _strategy = new BaseTimeRewardStrategy<BaseReward>(_profile, _duration, _finder, _rewards, _unlocked, _claimed);
        
        _unlocked.OnAdded += x =>
        {
            Debug.Log("Unlocked: " + x.Id.ToString());
        };
        
        _claimed.OnAdded += x =>
        {
            Debug.Log("Claimed: " + x.Id.ToString());
        };
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            _strategy.Update();
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            _strategy.Claim();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            _strategy.Reset();
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
           Debug.Log($"profile:{_profile.FirstRewardTime?.ToString()}:{_profile.LatestRewardTime?.ToString()}");
           
           if (_profile.LatestRewardTime != null)
               Debug.Log($"Duration From: {_profile.AfterDuration}");
        }
    }
}
