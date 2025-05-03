using System;
using System.Collections.Generic;
using R3;
using Siberian25.Game.Characters.Enemies;
using Random = UnityEngine.Random;

namespace Siberian25.Game.World
{
    public class GameRoom
    {
        public GameRoomComposition Composition => _composition;
        public GameRoomSchedule Schedule => _schedule;
        public IReadOnlyList<EnemyComposition> Enemies => _enemies;
        public EnemiesFactory EnemiesFactory => _enemiesFactory;
        public bool EnemiesExists => _enemies.Count > 0;
        
        private readonly GameRoomComposition _composition;
        private readonly GameRoomSchedule _schedule;
        private readonly EnemiesFactory _enemiesFactory;
        private readonly List<EnemyComposition> _enemies;
        private readonly Dictionary<EnemyComposition, IDisposable> _disposables;

        public GameRoom(GameRoomComposition composition)
        {
            _composition = composition;
            _schedule = composition.PossibleSchedules[Random.Range(0, composition.PossibleSchedules.Count)];
            _enemiesFactory = new(this);
            _enemies = new();
            _disposables = new();
        }
        
        public void RegisterEnemy(EnemyComposition enemy)
        {
            _disposables.Add(enemy, enemy.Health.OnDeath.Subscribe(_ => UnregisterEnemy(enemy)));
            _enemies.Add(enemy);
        }

        private void UnregisterEnemy(EnemyComposition enemy)
        {
            _disposables[enemy].Dispose();
            _disposables.Remove(enemy);
            _enemies.Remove(enemy);
        }
    }
}