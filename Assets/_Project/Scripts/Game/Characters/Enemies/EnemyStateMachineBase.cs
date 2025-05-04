using TSS.Core;
using UnityEngine;

namespace Siberian25.Game.Characters.Enemies
{
    public abstract class EnemyStateMachineBase : MonoBehaviour
    {
        public abstract void Run();
        public abstract void Stop();
        public abstract void UpdateSM();
        public abstract void FixedUpdateSM();
    }

    public abstract class EnemyStateMachineBase<T> : EnemyStateMachineBase
        where T : EnemyState
    {
        private T _activeState;

        public override void Run()
        {
            _activeState = GetFirstState();
            _activeState?.OnEnter();
        }
        public override void Stop()
        {
            _activeState?.OnExit();
            _activeState = null;
        }
        public void SwitchState(T state)
        {
            _activeState?.OnExit();
            _activeState = state;
            _activeState?.OnEnter();
        }

        public override void UpdateSM()
        {
            if (Runtime.IsPaused)
                return;
            _activeState?.OnUpdate();
        }

        public override void FixedUpdateSM()
        {
            if (Runtime.IsPaused)
                return;
            _activeState?.OnFixedUpdate();
        } 

        protected abstract T GetFirstState();
        
    }
}