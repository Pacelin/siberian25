using TSS.Core;

namespace Siberian25.Game.Characters.Boss
{
    public class BossStateMachine
    {
        public bool BossDamaged => _composition.PhaseIndex != _lastStatePhase;
        public int LastStatePhase => _lastStatePhase;
        public bool BossDeath => !_composition.LeftHeadAlive && !_composition.RightHeadAlive && !_composition.MiddleHeadAlive;
        public BossComposition Composition => _composition;

        private int _lastStatePhase;
        private readonly BossComposition _composition;
        private BossState _activeState;

        public BossStateMachine(BossComposition composition)
        {
            _composition = composition;
        }
        
        public void Run()
        {
            _activeState = new BossAppearState();
            _activeState.Init(this);
            _activeState.OnEnter();
        }

        public void Stop()
        {
            _activeState?.OnExit();
        }

        public void Update()
        {
            if (Runtime.IsPaused)
                return;
            _activeState?.OnUpdate();
        }
        
        public void SwitchState(BossState state)
        {
            _activeState?.OnExit();
            _activeState = state;
            _activeState?.Init(this);
            _lastStatePhase = Composition.PhaseIndex;
            _activeState?.OnEnter();
        }
    }
}