namespace Siberian25.Game.Characters.Boss
{
    public class BossDamagedState : BossState
    {
        public override void OnEnter()
        {
            if (Composition.LeftHead.IsAlive)
                Composition.LeftHead.StartAnimation(BossConstants.BOSS_DAMAGED_TRIGGER);
            if (Composition.MiddleHead.IsAlive)
                Composition.MiddleHead.StartAnimation(BossConstants.BOSS_DAMAGED_TRIGGER);
            if (Composition.RightHead.IsAlive)
                Composition.RightHead.StartAnimation(BossConstants.BOSS_DAMAGED_TRIGGER);
            foreach (var enemyComposition in Composition.CurrentPhase.BeforePhaseEnemies)
                GameContext.ActiveRoom.EnemiesFactory.Spawn(2, enemyComposition);
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate()
        {
            if (BossDeath)
                SwitchState(new BossDeathState());
            else if (BossDamaged)
                SwitchState(new BossDamagedState());
            else if (!GameContext.ActiveRoom.EnemiesExists)
                SwitchState(new BossIdleState());
        }
    }
}