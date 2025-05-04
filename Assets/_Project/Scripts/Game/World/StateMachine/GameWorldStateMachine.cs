using Cysharp.Threading.Tasks;
using Siberian25.Game.Characters;
using TSS.ContentManagement;
using TSS.Core;
using TSS.SceneManagement;

namespace Siberian25.Game.World
{
    public class GameWorldStateMachine
    {
        private GameWorldState _activeState;
        private bool _exit;
        
        public void Run()
        {
            _activeState = new GameWorldRoomStartState(GameContext.World.FirstRoomPrefab, true);
            _activeState.OnEnter();
        }

        public void Stop()
        {
            _activeState?.OnExit();
            _activeState = null;
        }

        public void Update()
        {
            if (Runtime.IsPaused)
                return;
            if (_exit)
                return;
            if (!GameContext.Player.Health.IsAlive)
            {
                GameContext.Player.StateMachine.SwitchState(new PlayerIdleState());
                GameContext.Player.StateMachine.SetPause(true);
                _exit = true;
                UniTask.Void(async () =>
                {
                    if (Runtime.CancellationToken.IsCancellationRequested)
                        return;
                    await GameContext.World.FadeOutWorld();
                    if (Runtime.CancellationToken.IsCancellationRequested)
                        return;
                    await SceneManager.Scene(CMS.Scenes.Game).Single().Load(Runtime.CancellationToken);
                });
                return;
            }
            _activeState?.OnUpdate();
        }

        public void SwitchState(GameWorldState state)
        {
            _activeState?.OnExit();
            _activeState = state;
            _activeState?.OnEnter();
        }
    }
}