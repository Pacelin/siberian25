using UnityEngine;

namespace Siberian25.Game.Characters.Enemies
{
    public class EnemyBugIdleState : EnemyBugState
    {
        private float _time;
        private float _duration;
        
        public EnemyBugIdleState(EnemyComposition composition, EnemyBugStateMachine stateMachine) : base(composition, stateMachine)
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
                StateMachine.SwitchState(new EnemyBugPrepareDashState(Composition, StateMachine));
        }

        public override void OnFixedUpdate()
        {
        }
    }
}