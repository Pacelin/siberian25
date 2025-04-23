using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace TSS.Achievements
{
    public static class Achievements
    {
        private static AchievementsCollection _collection;
        private static LocalAchievements _local;
        private static RemoteAchievements _remote;
        
        internal static void Initialize(AchievementsCollection collection)
        {
            _collection = collection;
            _local = new LocalAchievements(_collection);
            _local.Load();
            _remote = new RemoteAchievements(_local);
            _remote.Initialize();
        }

        internal static void Dispose()
        {
            _remote.Dispose();
        }

        public static IReadOnlyDictionary<string, AchievementConfig> Get() => _collection;
        public static Observable<string> ObserveAchievements() => _local.OnClaimAchievement;
        
        public static void Report(string achievement) => _local.AddReport(achievement);
        public static bool IsClaimed(string achievement) => _local.AchievementClaimed(achievement);
        
        public static void LoadRatio(string achievement, Action<float> onResult, Action onError)
        {
            if (!_remote.IsReachable)
            {
                onError?.Invoke();
                return;
            }
            
            UniTask.Void(async () =>
            {
                try
                {
                    var result = await _remote.GetClaimedRatio(achievement);
                    onResult?.Invoke(result);           
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    onError?.Invoke();
                }
            });
        }
    }
}