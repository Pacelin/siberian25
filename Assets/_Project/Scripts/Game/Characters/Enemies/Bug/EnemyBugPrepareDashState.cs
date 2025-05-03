using UnityEngine;

namespace Siberian25.Game.Characters.Enemies
{
    public class EnemyBugPrepareDashState : EnemyBugState
    {
        private float _time;
        private float _duration;
        public EnemyBugPrepareDashState(EnemyComposition composition, EnemyBugStateMachine stateMachine) : base(composition, stateMachine)
        {
        }

        public override void OnEnter()
        {
            _duration = StateMachine.DashPrepareDuration;
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate()
        {
            _time += Time.deltaTime;
            if (_time >= _duration)
                StateMachine.SwitchState(new EnemyBugDashState(Composition, StateMachine));
        }

        public override void OnFixedUpdate()
        {
        }
    }
}