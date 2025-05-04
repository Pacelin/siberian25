using TSS.Audio;
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
        private SoundEvent_Game_Beam.Instance _beam;

        public void StartAnimation(int trigger)
        {
            _animationFinished = false;
            _animator.SetTrigger(trigger);
        }
        private void OnDestroy()
        {
            _beam?.Stop(false);
            _beam?.Release();
        } 
        public void AnimationFinishEventHook() => _animationFinished = true;
        
        public void StartShortAttackSound() => AudioSystem.Game_BossAttack1.PlayOneShot();
        public void StartLongAttackSound() => AudioSystem.Game_BossAttack2.PlayOneShot();
        
        public void StartBeamSound()
        {
            _beam = AudioSystem.Game_Beam.CreateInstance();
            _beam.SetBeam(0);
            _beam.Start();
        }

        public void ApplyBeamSound()
        {
            _beam.SetBeam(1);
        }

        public void StopBeamSound()
        {
            _beam.Stop(true);
            _beam.Release();
            _beam = null;
        }
    }
}