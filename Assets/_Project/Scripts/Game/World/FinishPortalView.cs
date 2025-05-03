using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Siberian25.Game.Characters;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Siberian25.Game.World
{
    public class FinishPortalView : PortalView
    {
        public bool IsActive => _active;
        public IReadOnlyList<GameRoomComposition> Rooms => _rooms;

        [SerializeField] private GameRoomComposition[] _rooms;

        private bool _active;
        
        public override async UniTask Activate()
        {
            await base.Activate();
            _active = true;
        }

        public override UniTask Deactivate()
        {
            _active = false;
            return base.Deactivate();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!_active)
                return;
            if (col.TryGetComponent<PlayerComposition>(out _))
            {
                GameContext.World.StateMachine.SwitchState(new GameWorldRoomEndState(
                    _rooms[Random.Range(0, _rooms.Length)]));
            }
        }
    }
}