using UnityEngine;

namespace Siberian25.Game.Characters.Enemies
{
    public class EnemyBugIdleState : EnemyBugState
    {
        private float _time;
        
        public EnemyBugIdleState(EnemyComposition composition, EnemyBugStateMachine stateMachine) : base(composition, stateMachine)
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
            if (_time >= StateMachine.IdleDuration)
                StateMachine.SwitchState(new EnemyBugPrepareDashState(Composition, StateMachine));
        }

        public override void OnFixedUpdate()
        {
        }
    }
}