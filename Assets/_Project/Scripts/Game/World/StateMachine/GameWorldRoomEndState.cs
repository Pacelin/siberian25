using Cysharp.Threading.Tasks;
using Siberian25.Game.Characters;
using TSS.Audio;
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
                GameContext.Player.StateMachine.SetPause(true);
                GameContext.Player.StateMachine.SwitchState(new PlayerIdleState());
                GameContext.Player.Rigidbody.position = 
                    GameContext.ActiveRoom.Composition.FinishPortal.transform.position;
                GameContext.Player.DisappearTween.Play();
                AudioSystem.Game_portalIn.PlayOneShot();
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