using UnityEngine;

namespace Siberian25.Game.Characters
{
    public abstract class CharacterComposition : MonoBehaviour
    {
        public Rigidbody2D Rigidbody => _rigidbody;
        public Animator Animator => _animator;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
    }
}