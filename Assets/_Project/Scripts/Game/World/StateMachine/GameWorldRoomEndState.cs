using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Siberian25.Game.World
{
    public class GameWorldRoomEndState : GameWorldState
    {
        private readonly GameRoomComposition _nextRoom;
        public GameWorldRoomEndState(GameRoomComposition room)
        {
            _nextRoom = room;
        }
        
        public override void OnEnter()
        {
            GameContext.World.FadeOutWorld().ContinueWith(() =>
            {
                if (GameContext.CancellationToken.IsCancellationRequested)
                    return;
                var room = GameContext.ActiveRoom.Composition;
                Object.Destroy(room);
                SwitchState(new GameWorldRoomStartState(_nextRoom, false));
            });
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate()
        {
        }
    }
}