using R3;
using UnityEngine;

namespace Siberian25.Game.Characters
{
    public class HealthComponent : MonoBehaviour
    {
        public Observable<int> OnHealthChanged => _onHealthChanged;
        public Observable<int> OnDamage => _onDamage;
        public Observable<Unit> OnDeath => _onDeath;

        public int Health => _health;
        public bool IsAlive => _health > 0;
        public bool IsDead => _health == 0;

        [SerializeField] private int _health;

        private readonly Subject<int> _onHealthChanged = new();
        private readonly Subject<int> _onDamage = new();
        private readonly Subject<Unit> _onDeath = new();

        public void TakeDamage(int damage)
        {
            if (IsDead)
                return;
            _health = Mathf.Max(0, _health - damage);
            _onHealthChanged.OnNext(_health);
            _onDamage.OnNext(damage);
            if (_health == 0)
                _onDeath.OnNext(Unit.Default);
        }
    }
}