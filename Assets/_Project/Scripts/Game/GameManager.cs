using System;
using System.Threading;
using TSS.ContentManagement;
using TSS.Core;
using UnityEngine;
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
        }

        public void Tick()
        {
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}