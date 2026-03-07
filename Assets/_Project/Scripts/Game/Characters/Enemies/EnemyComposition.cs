using UnityEngine;

namespace Siberian25.Game.Characters.Enemies
{
    public class EnemyComposition : CharacterComposition
    {
        public bool Collision { get; set; }
        
        [SerializeField] private EnemyStateMachineBase _stateMachine;

        private void Start() => _stateMachine.Run();
        private void OnDestroy() => _stateMachine.Stop();
        private void Update() => _stateMachine.UpdateSM();
        private void FixedUpdate() => _stateMachine.FixedUpdateSM();
        private void OnCollisionEnter2D(Collision2D col) => Collision = true;
        private void OnCollisionStay2D(Collision2D col) => Collision = true;
    }
}