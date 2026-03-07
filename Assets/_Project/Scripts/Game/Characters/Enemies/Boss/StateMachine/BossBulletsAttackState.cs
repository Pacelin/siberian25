namespace Siberian25.Game.Characters.Boss
{
    public class BossBulletsAttackState : BossState
    {
        public override void OnEnter()
        {
            Composition.MiddleHead.StartAnimation(BossConstants.BOSS_BULLETS_TRIGGER);
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
            else if (Composition.MiddleHead.AnimationFinished)
                Composition.SwitchStateToSpawnEnemies();
        }
    }
}