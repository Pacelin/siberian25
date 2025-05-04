using UnityEngine;

namespace Siberian25.Game.Characters.Enemies
{
    public class EnemyWalkerIdleState : EnemyWalkerState
    {
        private float _time;
        private float _duration;
        
        public EnemyWalkerIdleState(EnemyComposition composition, EnemyWalkerStateMachine stateMachine) : base(composition, stateMachine)
        {
        }

        public override void OnEnter()
        {
            _duration = StateMachine.IdleDuration;
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate()
        {
            _time += Time.deltaTime;
            if (_time >= _duration)
                StateMachine.SwitchState(new EnemyWalkerWalkState(Composition, StateMachine));
        }

        public override void OnFixedUpdate()
        {
        }
    }
}