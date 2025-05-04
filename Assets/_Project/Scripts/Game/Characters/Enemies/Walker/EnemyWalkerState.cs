namespace Siberian25.Game.Characters.Enemies
{
    public abstract class EnemyWalkerState : EnemyState
    {
        protected readonly EnemyWalkerStateMachine StateMachine;
        protected EnemyWalkerState(EnemyComposition composition, EnemyWalkerStateMachine stateMachine) : base(composition) => StateMachine = stateMachine;
    }
}