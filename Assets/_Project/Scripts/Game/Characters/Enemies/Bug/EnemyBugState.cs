namespace Siberian25.Game.Characters.Enemies
{
    public abstract class EnemyBugState : EnemyState
    {
        protected readonly EnemyBugStateMachine StateMachine;
        protected EnemyBugState(EnemyComposition composition, EnemyBugStateMachine stateMachine) : base(composition) => StateMachine = stateMachine;
    }
}