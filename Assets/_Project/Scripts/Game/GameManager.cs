using System;
using System.Threading;
using Siberian25.Game.World;
using TSS.ContentManagement;
using TSS.Core;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Siberian25.Game
{
    public class GameManager : IInitializable, ITickable, IDisposable
    {
        [Inject] private WorldSlowMotion _slowMo;
        private CancellationTokenSource _cts;

        public void Initialize()
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(Runtime.CancellationToken);
            GameContext.CancellationToken = _cts.Token;
            GameContext.SlowMo = _slowMo;
            GameContext.Player = Object.Instantiate(CMS.Prefabs.Player, Vector3.zero, Quaternion.identity);
            GameContext.World = Object.Instantiate(CMS.Prefabs.World);
            GameContext.EnemiesPortalsPool = new ObjectPool<PortalView>(
                () => Object.Instantiate(GameContext.World.EnemyPortalPrefab));
            GameContext.HUD = Object.Instantiate(CMS.Prefabs.HUD);
            Time.timeScale = 1;
        }

        public void Tick()
        {
            if (_cts == null)
                return;
            _slowMo.Update();
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