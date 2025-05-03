namespace Siberian25.Game.World
{
    public abstract class GameWorldState
    {
        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void OnUpdate();

        protected void SwitchState(GameWorldState state) => GameContext.World.StateMachine.SwitchState(state);
    }
}