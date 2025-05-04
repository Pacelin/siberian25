using UnityEngine;

namespace Siberian25.Game.Characters
{
    public class PlayerMoveState : PlayerState
    {
        private float _stepTimestamp;
        private const float STEP_INTERVAL = 0.3f;
        
        public override void OnEnter()
        {
            GameContext.Player.Animator.SetBool(PlayerConstants.ANIMATOR_WALK_BOOL, true);
            _stepTimestamp = Time.time + STEP_INTERVAL;
        }

        public override void OnExit()
        {
            if (GameContext.CancellationToken.IsCancellationRequested)
                return;
            GameContext.Player.Animator.SetBool(PlayerConstants.ANIMATOR_WALK_BOOL, false);
        }

        public override void OnUpdate()
        {
            if (Time.time > _stepTimestamp)
            {
                _stepTimestamp = Time.time + STEP_INTERVAL;
                GameContext.ActiveRoom.Composition.StepSound.PlayOneShot();
            }
                
            if (Input.IsDashing && Composition.CanDash())
                SwitchState(new PlayerDashState());
            else if (Input.IsIdle)
                SwitchState(new PlayerIdleState());
            else
            {
                Animator.SetFloat(PlayerConstants.ANIMATOR_WALK_FLOAT_X, Input.MoveInput.x);
                Animator.SetFloat(PlayerConstants.ANIMATOR_WALK_FLOAT_Y, Input.MoveInput.y);
                Animator.SetBool(PlayerConstants.ANIMATOR_WALK_HORIZONTAL_BOOL, Input.MoveInput.x != 0);
            }
        }

        public override void OnFixedUpdate()
        {
            Rigidbody.MovePosition(Rigidbody.position + 
                Input.MoveInput * (Config.MovementSpeed * Time.fixedDeltaTime));
        }
    }
}