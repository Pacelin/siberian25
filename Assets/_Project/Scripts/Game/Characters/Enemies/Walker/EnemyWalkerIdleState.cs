using TSS.Audio;
using UnityEngine;

namespace Siberian25.Game.Characters.Enemies
{
    public class EnemyWalkerIdleState : EnemyWalkerState
    {
        private float _time;
        private float _duration;
        private SoundEvent_Game_FlyEnemyIdle.Instance _instance;
        
        public EnemyWalkerIdleState(EnemyComposition composition, EnemyWalkerStateMachine stateMachine) : base(composition, stateMachine)
        {
        }

        public override void OnEnter()
        {
            _duration = StateMachine.IdleDuration;
            _instance = AudioSystem.Game_FlyEnemyIdle.CreateInstance();
            _instance.Start();
        }

        public override void OnExit()
        {
            _instance.Stop(true);
            _instance.Release();
        }

        public override void OnUpdate()
        {
            _time += Time.deltaTime;
            if (_time >= _duration)
                StateMachine.SwitchState(new EnemyWalkerWalkState(Composition, StateMachine));
        }

        public override void OnFixedUpdate()
        {
        }
    }
}