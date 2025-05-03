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
            UniTask.Void(async () =>
            {
                GameContext.Player.DisappearTween.Play();
                await GameContext.Player.DisappearTween.WaitWhilePlay();
                if (GameContext.CancellationToken.IsCancellationRequested)
                    return;
                await GameContext.ActiveRoom.Composition.FinishPortal.Deactivate();
                if (GameContext.CancellationToken.IsCancellationRequested)
                    return;
                await GameContext.World.FadeOutWorld();
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