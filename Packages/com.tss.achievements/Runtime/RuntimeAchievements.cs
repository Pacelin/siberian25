using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using TSS.Core;
using UnityEngine.AddressableAssets;

namespace TSS.Achievements
{
    [RuntimeOrder(ERuntimeOrder.SystemRegistration, 1)]
    [UsedImplicitly]
    public class RuntimeAchievements : IRuntimeLoader
    {
        public async UniTask Initialize(CancellationToken cancellationToken)
        {
            var collection = await Addressables.LoadAssetAsync<AchievementsCollection>("Achievements Collection");
            Achievements.Initialize(collection);
            var collectionView = UnityEngine.Object.Instantiate(collection.ViewPrefab);
            UnityEngine.Object.DontDestroyOnLoad(collectionView.gameObject);
        }

        public void Dispose()
        {
            Achievements.Dispose();
        }
    }
}