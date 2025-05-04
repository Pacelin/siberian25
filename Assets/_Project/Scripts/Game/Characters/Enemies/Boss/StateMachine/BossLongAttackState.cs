using UnityEngine;

namespace Siberian25.Game.Characters.Boss
{
    public class BossLongAttackState : BossState
    {
        private BossHead _attackHead;
        
        public override void OnEnter()
        {
            if (Composition.LeftHeadAlive && Composition.RightHeadAlive)
                _attackHead = Random.Range(0, 2) == 0 ? Composition.RightHead : Composition.LeftHead;
            else if (Composition.LeftHeadAlive)
                _attackHead = Composition.LeftHead;
            else
                _attackHead = Composition.RightHead;
            _attackHead.StartAnimation(BossConstants.BOSS_LONG_ATTACK_TRIGGER);
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
            else if (_attackHead.AnimationFinished)
                Composition.SwitchStateToSpawnEnemies();
        }
    }
}