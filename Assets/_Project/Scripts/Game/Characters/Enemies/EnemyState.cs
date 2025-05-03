namespace Siberian25.Game.Characters.Enemies
{
    public abstract class EnemyState
    {
        protected readonly EnemyComposition Composition;
        
        protected EnemyState(EnemyComposition composition)
        {
            Composition = composition;
        }
        
        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();
    }
}