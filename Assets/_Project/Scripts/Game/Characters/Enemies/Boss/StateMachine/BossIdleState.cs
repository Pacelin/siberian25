using UnityEngine;

namespace Siberian25.Game.Characters.Boss
{
    public class BossIdleState : BossState
    {
        private float _duration;
        
        public override void OnEnter()
        {
            if (Composition.LeftHead.IsAlive)
                Composition.LeftHead.StartAnimation(BossConstants.BOSS_IDLE_TRIGGER);
            if (Composition.MiddleHead.IsAlive)
                Composition.MiddleHead.StartAnimation(BossConstants.BOSS_IDLE_TRIGGER);
            if (Composition.RightHead.IsAlive)
                Composition.RightHead.StartAnimation(BossConstants.BOSS_IDLE_TRIGGER);
            _duration = Random.Range(Composition.IdleDurationRange.x, Composition.IdleDurationRange.y);
        }

        public override void OnExit()
        {
            
        }

        public override void OnUpdate()
        {
            _duration -= Time.deltaTime;
            if (BossDeath)
                SwitchState(new BossDeathState());
            else if (BossDamaged)
                SwitchState(new BossDamagedState());
            else if (_duration <= 0)
                Composition.SwitchStateToAttack();
        }
    }
}