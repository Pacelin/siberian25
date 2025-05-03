namespace Siberian25.Game.World
{
    public class GameWorldStateMachine
    {
        private GameWorldState _activeState;
        
        public void Run()
        {
            _activeState = new GameWorldStartState();
            _activeState.OnEnter();
        }

        public void Stop()
        {
            _activeState?.OnExit();
            _activeState = null;
        }

        public void Update()
        {
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