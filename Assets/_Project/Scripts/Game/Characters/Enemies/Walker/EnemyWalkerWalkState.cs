using UnityEngine;

namespace Siberian25.Game.Characters.Enemies
{
    public class EnemyWalkerWalkState : EnemyWalkerState
    {
        private Vector2 _targetPosition;
        private float _time;
        private float _duration;
        
        public EnemyWalkerWalkState(EnemyComposition composition, EnemyWalkerStateMachine stateMachine) : base(composition, stateMachine)
        {
        }

        public override void OnEnter()
        {
            Composition.Collision = false;
            var randomVector = Random.insideUnitCircle.normalized * StateMachine.WalkDistance;
            var position = Composition.transform.position;
            var cast = Physics2D.Raycast(position, 
                randomVector.normalized, randomVector.magnitude, StateMachine.ObstaclesMask);

            _duration = 0.2f;

            if (cast)
                randomVector = Vector2.ClampMagnitude(randomVector, cast.distance);
            _targetPosition = Composition.Rigidbody.position + randomVector;
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate()
        {
            _duration -= Time.deltaTime;
            if (Composition.Rigidbody.position == _targetPosition || (_duration < 0f && Composition.Collision))
                StateMachine.SwitchState(new EnemyWalkerIdleState(Composition, StateMachine));
        }

        public override void OnFixedUpdate()
        {         
            Composition.Rigidbody.MovePosition(Vector2.MoveTowards(Composition.Rigidbody.position,
                _targetPosition, StateMachine.WalkSpeed * Time.fixedDeltaTime));
        }
    }
}