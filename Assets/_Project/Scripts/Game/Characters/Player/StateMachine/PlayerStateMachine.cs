namespace Siberian25.Game.Characters
{
    public class PlayerStateMachine
    {
        private PlayerState _activeState;

        public void Run()
        {
            _activeState = new PlayerIdleState();
            _activeState.OnEnter();
        }

        public void Stop()
        {
            _activeState?.OnExit();
            _activeState = null;
        }
        
        public void SwitchState(PlayerState state)
        {
            _activeState?.OnExit();
            _activeState = state;
            _activeState?.OnEnter();
        }

        public void Update()
        {
            _activeState?.OnUpdate();
        }

        public void FixedUpdate()
        {
            _activeState?.OnFixedUpdate();
        }
    }
}