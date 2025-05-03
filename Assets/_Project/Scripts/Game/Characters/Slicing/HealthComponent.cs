using System;
using System.Threading;
using Cysharp.Threading.Tasks;
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
        [SerializeField] private float _invinsibilityTimeOnDamage = .3f;

        private bool _canTakeDamage;

        private CancellationTokenSource _cts;

        private readonly Subject<int> _onHealthChanged = new();
        private readonly Subject<int> _onDamage = new();
        private readonly Subject<Unit> _onDeath = new();

        private void OnEnable()
        {
            _canTakeDamage = true;
            _cts = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        public void TakeDamage(int damage)
        {
            if (IsDead || !_canTakeDamage)
                return;

            _health = Mathf.Max(0, _health - damage);
            _onHealthChanged.OnNext(_health);
            _onDamage.OnNext(damage);

            if (_health == 0)
                _onDeath.OnNext(Unit.Default);

            AddInvincibility().Forget();
        }

        private async UniTaskVoid AddInvincibility()
        {
            _canTakeDamage = false;
            await UniTask.Delay(TimeSpan.FromSeconds(_invinsibilityTimeOnDamage), cancellationToken: _cts.Token);
            _canTakeDamage = true;
        }
    }
}