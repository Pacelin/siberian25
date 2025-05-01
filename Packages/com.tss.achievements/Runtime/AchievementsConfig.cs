using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using TSS.Achievements.View;
using TSS.Utils;
using UnityEngine;

namespace TSS.Achievements
{
    [CreateSingletonAsset("Assets/_Project/Configs/SO_Achievements.asset", "Achievements Config")]
    public class AchievementsConfig : ScriptableObject, IReadOnlyDictionary<string, AchievementConfig>
    {
        public AchievementNotificationCollectionView ViewPrefab => _viewPrefab;
        public AchievementsPanelView PanelPrefab => _panelPrefab;
        public string GrantApi => _grantApi;
        public string ListApi => _listApi;
        public string RatioApi => _ratioApi;
        public string ClearApi => _clearApi;
        
        [SerializeField] private AchievementNotificationCollectionView _viewPrefab;
        [SerializeField] private AchievementsPanelView _panelPrefab;
        [SerializedDictionary("Id", "Achievement")] 
        [SerializeField] private SerializedDictionary<string, AchievementConfig> _collection;
        [Header("Remote")]
        [SerializeField] private string _grantApi = "grant-achievement";
        [SerializeField] private string _listApi = "user-achievements";
        [SerializeField] private string _ratioApi = "achievements-ratio";
        [SerializeField] private string _clearApi = "clear-achievements";
        
        public int Count => _collection.Count;
        public IEnumerator<KeyValuePair<string, AchievementConfig>> GetEnumerator() => _collection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public bool ContainsKey(string key) => _collection.ContainsKey(key);
        public bool TryGetValue(string key, out AchievementConfig value) => _collection.TryGetValue(key, out value);
        public AchievementConfig this[string key] => _collection[key];
        public IEnumerable<string> Keys => _collection.Keys;
        public IEnumerable<AchievementConfig> Values => _collection.Values;
    }
}