namespace Siberian25.Game.Characters.Boss
{
    public abstract class BossState
    {
        public BossComposition Composition => _stateMachine.Composition;
        public int LastStatePhase => _stateMachine.LastStatePhase;
        public bool BossDamaged => _stateMachine.BossDamaged;
        public bool BossDeath => _stateMachine.BossDeath;
        
        private BossStateMachine _stateMachine;

        public void Init(BossStateMachine stateMachine) => _stateMachine = stateMachine;        
        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void OnUpdate();

        protected void SwitchState(BossState state) => _stateMachine.SwitchState(state);
    }
}