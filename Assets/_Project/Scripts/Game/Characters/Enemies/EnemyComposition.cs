using UnityEngine;

namespace Siberian25.Game.Characters.Enemies
{
    public class EnemyComposition : CharacterComposition
    {
        [SerializeField] private EnemyStateMachineBase _stateMachine;

        private void Start() => _stateMachine.Run();
        private void OnDestroy() => _stateMachine.Stop();
        private void Update() => _stateMachine.UpdateSM();
        private void FixedUpdate() => _stateMachine.FixedUpdateSM();
    }
}