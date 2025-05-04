namespace Siberian25.Game.Characters.Boss
{
    public class BossAppearState : BossState
    {
        public override void OnEnter()
        {
            Composition.WholeAnimationFinished = false;
            Composition.WholeAnimator.SetTrigger(BossConstants.BOSS_APPEAR_TRIGGER);
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate()
        {
            if (Composition.WholeAnimationFinished)
                SwitchState(new BossIdleState());
        }
    }
}