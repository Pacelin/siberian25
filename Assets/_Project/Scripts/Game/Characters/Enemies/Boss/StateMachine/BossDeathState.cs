namespace Siberian25.Game.Characters.Boss
{
    public class BossDeathState : BossState
    {
        public override void OnEnter()
        {
            Composition.WholeAnimator.SetTrigger(BossConstants.BOSS_DEATH_TRIGGER);
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate()
        {
        }
    }
}