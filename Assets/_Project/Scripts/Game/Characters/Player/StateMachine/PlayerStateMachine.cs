using TSS.Core;

namespace Siberian25.Game.Characters
{
    public class PlayerStateMachine
    {
        private PlayerState _activeState;
        private bool _paused;
        
        public void Run()
        {
            _activeState = new PlayerIdleState();
            _activeState.OnEnter();
        }

        public void SetPause(bool pause)
        {
            _paused = pause;
            GameContext.Player.Arrow.gameObject.SetActive(!pause);
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
            if (_paused)
                return;
            if (Runtime.IsPaused)
                return;
            _activeState?.OnUpdate();
        }

        public void FixedUpdate()
        {
            if (_paused)
                return;
            if (Runtime.IsPaused)
                return;
            _activeState?.OnFixedUpdate();
        }
    }
}