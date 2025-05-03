using UnityEngine;

namespace Siberian25.Game.Characters.Enemies
{
    public class EnemyBugPrepareDashState : EnemyBugState
    {
        private float _time;
        public EnemyBugPrepareDashState(EnemyComposition composition, EnemyBugStateMachine stateMachine) : base(composition, stateMachine)
        {
        }

        public override void OnEnter()
        {
            
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate()
        {
            _time += Time.deltaTime;
            if (_time >= StateMachine.DashPrepareDuration)
                StateMachine.SwitchState(new EnemyBugDashState(Composition, StateMachine));
        }

        public override void OnFixedUpdate()
        {
        }
    }
}