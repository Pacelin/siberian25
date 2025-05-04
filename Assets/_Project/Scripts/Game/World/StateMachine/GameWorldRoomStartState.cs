using Cysharp.Threading.Tasks;
using Siberian25.Game.Characters;
using TSS.Audio;
using UnityEngine;

namespace Siberian25.Game.World
{
    public class GameWorldRoomStartState : GameWorldState
    {
        private readonly GameRoomComposition _roomCompositionPrefab;
        private readonly bool _activatePlayerImmediate;
        
        public GameWorldRoomStartState(GameRoomComposition roomCompositionPrefab, bool activatePlayerImmediate)
        {
            _roomCompositionPrefab = roomCompositionPrefab;
            _activatePlayerImmediate = activatePlayerImmediate;
        }
        
        public override void OnEnter()
        {
            GameContext.ActiveRoom = new GameRoom(Object.Instantiate(_roomCompositionPrefab));
            if (_activatePlayerImmediate)
            {
                if (GameContext.CancellationToken.IsCancellationRequested)
                    return;
                GameContext.Player.transform.position =
                    GameContext.ActiveRoom.Composition.PlayerPortal.transform.position;
                SwitchState(new GameWorldRoomBattleState());
            }
            else
            {
                GameContext.World.FadeInWorld().ContinueWith(async () =>
                {
                    if (GameContext.CancellationToken.IsCancellationRequested)
                        return;
                    await GameContext.ActiveRoom.Composition.PlayerPortal.Activate();
                    GameContext.Player.transform.position =
                        GameContext.ActiveRoom.Composition.PlayerPortal.transform.position;
                    GameContext.OST.SetTrack(GameContext.ActiveRoom.Composition.Track);
                    GameContext.Player.AppearTween.Play();
                    AudioSystem.Game_portalOut.PlayOneShot();
                    if (GameContext.CancellationToken.IsCancellationRequested)
                        return;
                    await GameContext.Player.AppearTween.WaitWhilePlay(); 
                    
                    if (GameContext.CancellationToken.IsCancellationRequested)
                        return;
                    await GameContext.ActiveRoom.Composition.PlayerPortal.Deactivate();
                    if (GameContext.CancellationToken.IsCancellationRequested)
                        return;
                    GameContext.Player.StateMachine.SetPause(false);
                    SwitchState(new GameWorldRoomBattleState());
                });
            }
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate()
        {
        }
    }
}