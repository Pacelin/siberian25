using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace TSS.Achievements.View
{
    public class AchievementNotificationCollectionView : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<EAchievementNotificationContainer, AchievementNotificationView> _notificationsViews;

        //private Dictionary<EAchievementNotificationContainer, UniTaskCompletionSource> _completions = new();
        private Dictionary<EAchievementNotificationContainer, UniTask> _tasks = new();
        private IDisposable _disposable;
        
        private void OnEnable()
        {
            _disposable = Achievements.ObserveAchievements()
                //.Subscribe(key => RecieveNotification(key).Forget());
                .Subscribe(RecieveNotification);
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
            //_completions.Clear();
            _tasks.Clear();
        }

        private void RecieveNotification(string achievementKey)
        {
            var achievement = Achievements.Get()[achievementKey];
            var container = achievement.NotificationContainer;
            var oldTask = _tasks.ContainsKey(container) ? _tasks[container] : UniTask.CompletedTask;

            _tasks[container] = oldTask.ContinueWith(() => _notificationsViews[container].Claim(achievement));
            _tasks[container].Forget();
        }
        /*
        private async UniTaskVoid RecieveNotification(string achievementKey)
        {
            var achievement = Achievements.Get()[achievementKey];
            var container = achievement.NotificationContainer;
            var oldCompletionSource = _completions.ContainsKey(container) ? _completions[container] : null;
            var completionSource = new UniTaskCompletionSource();
            
            _completions[container] = completionSource;
            if (oldCompletionSource != null)
                await oldCompletionSource.Task;

            await _notificationsViews[container].Claim(achievement);
            completionSource.TrySetResult();
        }*/
    }
}