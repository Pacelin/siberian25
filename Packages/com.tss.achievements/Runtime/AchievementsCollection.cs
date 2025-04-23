using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using TSS.Achievements.View;
using TSS.Utils;
using UnityEngine;

namespace TSS.Achievements
{
    [CreateSingletonAsset("Assets/_Project/Configs/SO_Achievements.asset", "Achievements Collection")]
    public class AchievementsCollection : ScriptableObject, IReadOnlyDictionary<string, AchievementConfig>
    {
        public AchievementNotificationCollectionView ViewPrefab => _viewPrefab;
        
        [SerializeField] private AchievementNotificationCollectionView _viewPrefab;
        [SerializedDictionary("Id", "Achievement")] 
        [SerializeField] private SerializedDictionary<string, AchievementConfig> _collection;

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