using UnityEngine;

namespace Siberian25.Game.Characters.Enemies
{
    public class EnemyBugStateMachine : EnemyStateMachineBase<EnemyBugState>
    {
        public float DashSpeed => _dashSpeed;
        public float DashSpread => _dashSpread;
        public float MaxDashDistance => _maxDashDistance;
        public float DashDistanceOverPlayer => _dashDistanceOverPlayer;
        public float IdleDuration => _idleDuration;
        public float DashPrepareDuration => _dashPrepareDuration;
        public LayerMask ObstaclesMask => _obstaclesMask;
        public float ObstacleAvoidRadius => _obstacleAvoidRadius;
        public TrailRenderer DashTrail => _dashTrail;
        public EnemyDamageProvider DamageProvider => _damageProvider;
        
        [SerializeField] private EnemyComposition _composition;
        [SerializeField] private TrailRenderer _dashTrail;
        [SerializeField] private EnemyDamageProvider _damageProvider;
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _dashSpread;
        [SerializeField] private float _maxDashDistance;
        [SerializeField] private float _dashDistanceOverPlayer = 2f;
        [SerializeField] private float _idleDuration;
        [SerializeField] private float _dashPrepareDuration;
        [SerializeField] private LayerMask _obstaclesMask;
        [SerializeField] private float _obstacleAvoidRadius;

        protected override EnemyBugState GetFirstState() => new EnemyBugIdleState(_composition, this);
    }
}