using UnityEngine;

namespace Siberian25.Game.Characters
{
    [CreateAssetMenu(menuName = "Game/Character", fileName = "SO_Enemy")]
    public class CharacterConfig : ScriptableObject
    {
        public float MovementSpeed => _movementSpeed;
        
        [Header("Character")]
        [SerializeField] private float _movementSpeed;
    }
}