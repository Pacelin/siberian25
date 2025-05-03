using UnityEngine;

namespace Siberian25.Game.Characters
{
    public static class PlayerConstants
    {
        public static int ANIMATOR_IDLE_BOOL = Animator.StringToHash("idle");
        public static int ANIMATOR_WALK_BOOL = Animator.StringToHash("walk");
        public static int ANIMATOR_DASH_BOOL = Animator.StringToHash("dash");
        public static int ANIMATOR_DASH_FLOAT_X = Animator.StringToHash("dash_x");
        public static int ANIMATOR_DASH_FLOAT_Y = Animator.StringToHash("dash_y");
        public static int ANIMATOR_WALK_FLOAT_X = Animator.StringToHash("walk_x");
        public static int ANIMATOR_WALK_FLOAT_Y = Animator.StringToHash("walk_y");
    }
}