using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Siberian25.Game.Characters
{
    public class PlayerDashState : PlayerState
    {
        private Vector2 _dashPosition;
        
        public override void OnEnter()
        {
            Composition.SliceTrigger.StartDash();
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
            Animator.SetFloat(PlayerConstants.ANIMATOR_WALK_FLOAT_X, dashVector.x);
            Animator.SetFloat(PlayerConstants.ANIMATOR_WALK_FLOAT_Y, dashVector.y);
            Animator.SetFloat(PlayerConstants.ANIMATOR_DASH_FLOAT_X, dashVector.x);
            Animator.SetFloat(PlayerConstants.ANIMATOR_DASH_FLOAT_Y, dashVector.y);
            Animator.SetBool(PlayerConstants.ANIMATOR_DASH_HORIZONTAL_BOOL, Mathf.Abs(dashVector.normalized.x) > 0.2f);
            Animator.SetBool(PlayerConstants.ANIMATOR_DASH_BOOL, true);
        }

        public override void OnExit()
        {
            Composition.SliceTrigger.PerformSlice();
            GameContext.Player.DashTrail.emitting = false;
            Composition.SliceTrigger.EnableSlice = false;
            if (!Composition.SliceTrigger.WasSliceCurrentDash())
                Composition.StartDashCooldown();
            Animator.SetBool(PlayerConstants.ANIMATOR_DASH_BOOL, false);
        }

        public override void OnUpdate()
        {
            if (Composition.SliceTrigger.WasSliceCurrentDash())
            {
                if (Input.IsDashing)
                    SwitchState(new PlayerDashState());
            }
            if (Rigidbody.position == _dashPosition)
            {
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