using UnityEngine;

namespace Siberian25.Game.Characters.Boss
{
    public static class BossConstants
    {
        public static int BOSS_IDLE_TRIGGER = Animator.StringToHash("idle");
        public static int BOSS_LASER_TRIGGER = Animator.StringToHash("laser");
        public static int BOSS_BULLETS_TRIGGER = Animator.StringToHash("bullets");
        public static int BOSS_LONG_ATTACK_TRIGGER = Animator.StringToHash("long_attack");
        public static int BOSS_SHORT_ATTACK_TRIGGER = Animator.StringToHash("short_attack");
        public static int BOSS_DAMAGED_TRIGGER = Animator.StringToHash("boss_damaged");
        public static int BOSS_SPAWN_TRIGGER = Animator.StringToHash("boss_spawn");
        public static int BOSS_DEATH_TRIGGER = Animator.StringToHash("boss_death");
    }
}