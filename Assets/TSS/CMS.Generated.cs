// Auto-generated code. Reference: "Packages/com.tss.cms/Editor/CMSGenerator.cs"

// ReSharper disable RedundantUsingDirective
#pragma warning disable CS1998

using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using JetBrains.Annotations;
using UnityEngine;
using Siberian25.Game.Characters;
using Siberian25.Game.World;
using TSS.Core;

namespace TSS.ContentManagement
{
    [PublicAPI]
    [UsedImplicitly]
    [RuntimeOrder(ERuntimeOrder.SystemRegistration)]
    public class CMS : IRuntimeLoader
    {
 

        public async UniTask Initialize(CancellationToken cancellationToken)
        {
			await Scenes.Initialize(cancellationToken);
			await Prefabs.Initialize(cancellationToken);
        }

        public void Dispose() { }

		[PublicAPI]
		public static class Scenes
		{
			public const string MainMenu = "Assets/Scenes/1_Menu.unity";
			public const string Game = "Assets/Scenes/2_Game.unity";

			public static async UniTask Initialize(CancellationToken cancellationToken)
			{
			}
		}
		[PublicAPI]
		public static class Prefabs
		{
			public static PlayerComposition Player { get; private set; }
			public static GameWorldComposition World { get; private set; }
			public static GameObject HUD { get; private set; }

			public static async UniTask Initialize(CancellationToken cancellationToken)
			{
				Player = (await Addressables.LoadAssetAsync<GameObject>("Assets/_Project/Content/Game/Characters/P_Player.prefab")
					.ToUniTask(cancellationToken: cancellationToken)).GetComponent<PlayerComposition>();
				World = (await Addressables.LoadAssetAsync<GameObject>("Assets/_Project/Content/Game/World/P_World.prefab")
					.ToUniTask(cancellationToken: cancellationToken)).GetComponent<GameWorldComposition>();
				HUD = await Addressables.LoadAssetAsync<GameObject>("Assets/_Project/Content/Game/World/P_HUD.prefab")
					.ToUniTask(cancellationToken: cancellationToken);
			}
		}
    }
}