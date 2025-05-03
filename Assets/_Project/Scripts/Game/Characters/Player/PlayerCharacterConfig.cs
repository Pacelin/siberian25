using UnityEngine;

namespace Siberian25.Game.Characters
{
    [CreateAssetMenu(menuName = "Game/Player Character", fileName = "SO_Player")]
    public class PlayerCharacterConfig : CharacterConfig
    {
        public float MaxAttackDashDistance => _maxAttackDashDistance;
        public float AttackDashSpeed => _attackDashSpeed;

        public float DashCooldown => _dashCooldown;
        
        public LayerMask DashObstaclesLayerMask => _dashObstaclesLayerMask;
        public float DashObstacleAviodRadius => _dashObstacleAvoidRadius;
        
        [Header("Player")] 
        [SerializeField] private float _maxAttackDashDistance;
        [SerializeField] private float _attackDashSpeed;
        [SerializeField] private float _dashCooldown;
        [Space]
        [SerializeField] private LayerMask _dashObstaclesLayerMask;
        [SerializeField] private float _dashObstacleAvoidRadius;
    }
}