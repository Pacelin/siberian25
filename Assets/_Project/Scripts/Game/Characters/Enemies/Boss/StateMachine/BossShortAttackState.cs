using System.Linq;
using UnityEngine;

namespace Siberian25.Game.Characters.Boss
{
    public class BossShortAttackState : BossState
    {
        private BossHead[] _shortAttackHeads;
        
        public override void OnEnter()
        {
            bool bothHeads = Composition.LeftHeadAlive && Composition.RightHeadAlive &&
                             Random.Range(0f, 1f) <= Composition.ShortAttackBothHeadsChance;
            if (bothHeads)
                _shortAttackHeads = new[] { Composition.LeftHead, Composition.RightHead };
            else if (Composition.LeftHeadAlive && Composition.RightHeadAlive)
                _shortAttackHeads = new[] { Random.Range(0, 2) == 0 ? Composition.LeftHead : Composition.RightHead };
            else if (Composition.LeftHeadAlive)
                _shortAttackHeads = new[] { Composition.LeftHead };
            else
                _shortAttackHeads = new[] { Composition.RightHead };

            foreach (var shortAttackHead in _shortAttackHeads)
                shortAttackHead.StartAnimation(BossConstants.BOSS_SHORT_ATTACK_TRIGGER);
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate()
        {
            var allFinished = _shortAttackHeads.All(h => h.AnimationFinished);
            if (BossDeath)
                SwitchState(new BossDeathState());
            else if (BossDamaged)
                SwitchState(new BossDamagedState());
            else if (allFinished)
                Composition.SwitchStateToSpawnEnemies();
        }
    }
}