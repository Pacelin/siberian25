using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Siberian25.Game.Characters
{
    public class PlayerDashState : PlayerState
    {
        private Vector2 _dashPosition;
        
        public override void OnEnter()
        {
            var dashVector = Vector2.ClampMagnitude(PlayerPointer.GetVector(PlayerPointer.GetPosition()), 
                Config.MaxAttackDashDistance);

            var cast = Physics2D.CircleCast(Rigidbody.position, Config.DashObstacleAviodRadius,
                dashVector.normalized, dashVector.magnitude, Config.DashObstaclesLayerMask);
            if (cast)
                dashVector = Vector2.ClampMagnitude(dashVector, cast.distance);
            _dashPosition = Rigidbody.position + dashVector;
            
            Composition.DashTrail.emitting = true;
            Composition.SliceTrigger.SliceDirection = dashVector.normalized;
            Composition.SliceTrigger.EnableSlice = true;
            Animator.SetBool(PlayerConstants.ANIMATOR_DASH_BOOL, true);
        }

        public override void OnExit()
        {
            GameContext.Player.DashTrail.emitting = false;
            Composition.SliceTrigger.EnableSlice = false;
            Animator.SetBool(PlayerConstants.ANIMATOR_DASH_BOOL, false);
        }

        public override void OnUpdate()
        {
            if (Rigidbody.position == _dashPosition)
            {
                Composition.SliceTrigger.PerformSlice();
                if (Input.IsIdle)
                    SwitchState(new PlayerIdleState());
                else
                    SwitchState(new PlayerMoveState());
            }
        }

        public override void OnFixedUpdate()
        {
            Rigidbody.MovePosition(Vector2.MoveTowards(Rigidbody.position, _dashPosition, Config.AttackDashSpeed * Time.fixedDeltaTime));
        }
    }
}