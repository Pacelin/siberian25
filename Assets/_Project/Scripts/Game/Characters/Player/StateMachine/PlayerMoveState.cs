using UnityEngine;

namespace Siberian25.Game.Characters
{
    public class PlayerMoveState : PlayerState
    {
        public override void OnEnter()
        {
            GameContext.Player.Animator.SetBool(PlayerConstants.ANIMATOR_WALK_BOOL, true);
        }

        public override void OnExit()
        {
            GameContext.Player.Animator.SetBool(PlayerConstants.ANIMATOR_WALK_BOOL, false);
        }

        public override void OnUpdate()
        {
            if (Input.IsDashing && Composition.CanDash())
                SwitchState(new PlayerDashState());
            else if (Input.IsIdle)
                SwitchState(new PlayerIdleState());
        }

        public override void OnFixedUpdate()
        {
            Rigidbody.MovePosition(Rigidbody.position + 
                Input.MoveInput * (Config.MovementSpeed * Time.fixedDeltaTime));
        }
    }
}