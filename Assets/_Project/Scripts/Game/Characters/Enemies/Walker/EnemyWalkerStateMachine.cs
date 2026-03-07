using UnityEngine;

namespace Siberian25.Game.Characters.Enemies
{
    public class EnemyWalkerStateMachine : EnemyStateMachineBase<EnemyWalkerState>
    {
        public float IdleDuration => Random.Range(_idleDuration.x, _idleDuration.y);
        public LayerMask ObstaclesMask => _obstaclesMask;
        public float ObstacleAvoidRadius => _obstacleAvoidRadius;

        public Vector2 WalkDistance => _walkDistance;
        public float WalkSpeed => _walkSpeed;
        
        [SerializeField] private EnemyComposition _composition;
        [SerializeField] private Vector2 _walkDistance;
        [SerializeField] private float _walkSpeed;
        [SerializeField] private Vector2 _idleDuration;
        [SerializeField] private LayerMask _obstaclesMask;
        [SerializeField] private float _obstacleAvoidRadius;

        protected override EnemyWalkerState GetFirstState() => new EnemyWalkerIdleState(_composition, this);
    }
}