using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Postgrest;
using R3;
using TSS.Achievements.Database;
using TSS.Core;
using TSS.Supabase;
using TSS.Supabase.Data;
using UnityEngine;

namespace TSS.Achievements
{
    internal class RemoteAchievements
    {
        private struct AchievementTimestamp
        {
            public float Time;
            public int ClaimsCount;
        }

        private struct UserTimestamp
        {
            public float Time;
            public int UsersCount;
        }

        public bool IsReachable => SupabaseManager.IsInitialized;
        
        private const float UPDATE_FREQ = 30f;
        
        private IDisposable _localDisposable;
        
        private readonly LocalAchievements _local;
        private readonly Dictionary<string, AchievementTimestamp> _achievementTimestamps;
        private UserTimestamp _userTimestamp;
        
        public RemoteAchievements(LocalAchievements local)
        {
            _local = local;
            _achievementTimestamps = new Dictionary<string, AchievementTimestamp>();
        }

        public void Initialize()
        {
            _localDisposable = _local.OnClaimAchievement.Subscribe(key => OnClaimLocalAchievement(key).Forget());
            SyncLocal().Forget();
        }

        public void Dispose() => _localDisposable.Dispose();

        public async UniTask<float> GetClaimedRatio(string achievement)
        {
            if (Time.time - _userTimestamp.Time >= UPDATE_FREQ)
                await FetchUsers();
            if (!_achievementTimestamps.ContainsKey(achievement) ||
                Time.time - _achievementTimestamps[achievement].Time >= UPDATE_FREQ)
                await FetchAchievement(achievement);
            return 1f * _achievementTimestamps[achievement].ClaimsCount / _userTimestamp.UsersCount;
        }

        private async UniTask FetchUsers()
        {
            var count = await SupabaseManager.Client.From<User>()
                .Count(Constants.CountType.Exact, Runtime.CancellationToken);
            _userTimestamp = new UserTimestamp()
            {
                UsersCount = count,
                Time = Time.time
            };
        }

        private async UniTask FetchAchievement(string achievement)
        {
            var count = await SupabaseManager.Client.From<UserAchievement>()
                .Where(a => a.AchievementId == achievement)
                .Count(Constants.CountType.Exact, Runtime.CancellationToken);
            _achievementTimestamps[achievement] = new AchievementTimestamp()
            {
                ClaimsCount = count,
                Time = Time.time
            };
        }

        private async UniTaskVoid SyncLocal()
        {
            var result = await SupabaseManager.Client.From<UserAchievement>()
                .Where(a => a.UserId == SupabaseManager.CurrentUserId)
                .Get(Runtime.CancellationToken);
            var claimedRemote = result.Models.Select(a => a.AchievementId);
            var claimedLocal = _local.ClaimedAchievements;
            var notClaimedRemote = claimedLocal.Except(claimedRemote)
                .Select(key => new UserAchievement(key));
            await SupabaseManager.Client.From<UserAchievement>()
                .Insert(notClaimedRemote.ToArray(), cancellationToken: Runtime.CancellationToken);
        }
        
        private async UniTaskVoid OnClaimLocalAchievement(string key)
        {
            if (_achievementTimestamps.ContainsKey(key))
            {
                var newTimestamp = new AchievementTimestamp()
                {
                    Time = Time.time,
                    ClaimsCount = _achievementTimestamps[key].ClaimsCount + 1
                };
                _achievementTimestamps[key] = newTimestamp;
            }
            await SupabaseManager.Client.From<UserAchievement>()
                .Insert(new UserAchievement(key), cancellationToken: Runtime.CancellationToken);
        }
    }
}