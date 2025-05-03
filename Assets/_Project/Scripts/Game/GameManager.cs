using System;
using System.Threading;
using Siberian25.Game.World;
using TSS.ContentManagement;
using TSS.Core;
using UnityEngine;
using UnityEngine.Pool;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Siberian25.Game
{
    public class GameManager : IInitializable, ITickable, IDisposable
    {
        private CancellationTokenSource _cts;

        public void Initialize()
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(Runtime.CancellationToken);
            GameContext.CancellationToken = _cts.Token;
            GameContext.Player = Object.Instantiate(CMS.Prefabs.Player, Vector3.zero, Quaternion.identity);
            GameContext.World = Object.Instantiate(CMS.Prefabs.World);
            GameContext.EnemiesPortalsPool = new ObjectPool<PortalView>(
                () => Object.Instantiate(GameContext.World.EnemyPortalPrefab));
            GameContext.HUD = Object.Instantiate(CMS.Prefabs.HUD);
            Time.timeScale = 1;
        }

        public void Tick()
        {
        }

        public void Dispose()
        {
            Time.timeScale = 1;
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}