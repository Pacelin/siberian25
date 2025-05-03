using UnityEngine;

namespace Siberian25.Game.Characters
{
    public class PlayerIdleState : PlayerState
    {
        public override void OnEnter()
        {
            Animator.SetBool(PlayerConstants.ANIMATOR_IDLE_BOOL, true);
        }

        public override void OnExit()
        {
            Animator.SetBool(PlayerConstants.ANIMATOR_IDLE_BOOL, false);
        }

        public override void OnUpdate()
        {
            Composition.Combo.Update();
            if (Input.IsDashing && Composition.CanDash())
                SwitchState(new PlayerDashState());
            else if (Input.IsMove)
                SwitchState(new PlayerMoveState());
        }

        public override void OnFixedUpdate()
        {
        }
    }
}