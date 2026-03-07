using TSS.Audio;
using UnityEngine;

namespace Siberian25.Game.Characters.Enemies
{
    public class EnemyBugDashState : EnemyBugState
    {
        private Vector2 _dashPosition;
        
        public EnemyBugDashState(EnemyComposition composition, EnemyBugStateMachine stateMachine) : base(composition, stateMachine)
        {
        }

        public override void OnEnter()
        {
            Composition.Collision = false;
            var position = (Vector2) GameContext.Player.DashTrail.transform.position;
            var playerPos = position;
            var dashVector = playerPos - (Vector2) StateMachine.DashTrail.transform.position;
            dashVector = dashVector.normalized.Spread(StateMachine.DashSpread) * dashVector.magnitude;
            dashVector += dashVector.normalized * StateMachine.DashDistanceOverPlayer;
            dashVector = Vector2.ClampMagnitude(dashVector, StateMachine.MaxDashDistance);

            var cast = Physics2D.Raycast(position, 
                dashVector.normalized, dashVector.magnitude, StateMachine.ObstaclesMask);
            if (cast)
                dashVector = Vector2.ClampMagnitude(dashVector, cast.distance);
            _dashPosition = Composition.Rigidbody.position + dashVector;

            AudioSystem.Game_MeleeEnemyAttack.PlayOneShot();
            StateMachine.DashTrail.emitting = true;
            StateMachine.DamageProvider.SetActive(true);
        }

        public override void OnExit()
        {
            if (GameContext.CancellationToken.IsCancellationRequested)
                return;
            GameContext.Player.DashTrail.emitting = false;
            StateMachine.DamageProvider.SetActive(false);
        }

        public override void OnUpdate()
        {
            if (Composition.Rigidbody.position == _dashPosition || Composition.Collision)
                StateMachine.SwitchState(new EnemyBugIdleState(Composition, StateMachine));
        }

        public override void OnFixedUpdate()
        {         
            Composition.Rigidbody.MovePosition(Vector2.MoveTowards(Composition.Rigidbody.position,
                _dashPosition, StateMachine.DashSpeed * Time.fixedDeltaTime));
        }
    }
}