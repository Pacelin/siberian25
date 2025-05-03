using Cysharp.Threading.Tasks;
using TSS.Tweening;
using UnityEngine;

namespace Siberian25.Game.Characters
{
    public abstract class CharacterComposition : MonoBehaviour
    {
        public Rigidbody2D Rigidbody => _rigidbody;
        public Animator Animator => _animator;
        public HealthComponent Health => _health;

        public ScriptableTween AppearTween => _appearTween;
        public ScriptableTween DisappearTween => _disappearTween;
        
        [SerializeField] private ScriptableTween _appearTween;
        [SerializeField] private ScriptableTween _disappearTween;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private HealthComponent _health;
    }
}