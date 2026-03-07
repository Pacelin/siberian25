using UnityEngine;

namespace Siberian25.Game.Characters.Boss
{
    public class BossSpawnEnemiesState : BossState
    {
        private float _duration;
        
        public override void OnEnter()
        {
            if (Composition.LeftHead.IsAlive)
                Composition.LeftHead.StartAnimation(BossConstants.BOSS_ENEMIES_TRIGGER);
            if (Composition.MiddleHead.IsAlive)
                Composition.MiddleHead.StartAnimation(BossConstants.BOSS_ENEMIES_TRIGGER);
            if (Composition.RightHead.IsAlive)
                Composition.RightHead.StartAnimation(BossConstants.BOSS_ENEMIES_TRIGGER);
            foreach (var phaseEnemy in Composition.CurrentPhase.PhaseEnemies)
                GameContext.ActiveRoom.EnemiesFactory.Spawn(2, phaseEnemy);
            _duration = Random.Range(
                Composition.CurrentPhase.SpawnEnemiesIdleRange.x,
                Composition.CurrentPhase.SpawnEnemiesIdleRange.y);
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