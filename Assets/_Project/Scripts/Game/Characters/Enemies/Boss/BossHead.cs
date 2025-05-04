using UnityEngine;

namespace Siberian25.Game.Characters.Boss
{
    public class BossHead : MonoBehaviour
    {
        public bool IsAlive => _health && _health.IsAlive;
        public bool AnimationFinished => _animationFinished;
        
        [SerializeField] private HealthComponent _health;
        [SerializeField] private Animator _animator;
        
        private bool _animationFinished;

        public void StartAnimation(int trigger)
        {
            _animationFinished = false;
            _animator.SetTrigger(trigger);
        }
        public void AnimationFinishEventHook() => _animationFinished = true;
    }
}