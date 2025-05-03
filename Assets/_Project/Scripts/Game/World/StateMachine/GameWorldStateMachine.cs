using TSS.Core;

namespace Siberian25.Game.World
{
    public class GameWorldStateMachine
    {
        private GameWorldState _activeState;
        
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