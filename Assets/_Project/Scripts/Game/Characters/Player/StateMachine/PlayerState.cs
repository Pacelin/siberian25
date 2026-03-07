using UnityEngine;

namespace Siberian25.Game.Characters
{
    public abstract class PlayerState
    {
        public static PlayerCharacterConfig Config => GameContext.Player.Config;
        public static PlayerInput Input => GameContext.Player.Input;
        public static PlayerComposition Composition => GameContext.Player;
        public static Rigidbody2D Rigidbody => GameContext.Player.Rigidbody;
        public static Animator Animator => GameContext.Player.Animator;
        
        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();

        protected void SwitchState(PlayerState state) => GameContext.Player.StateMachine.SwitchState(state);
    }
}