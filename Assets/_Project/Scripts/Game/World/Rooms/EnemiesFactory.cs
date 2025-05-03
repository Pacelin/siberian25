using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Siberian25.Game.Characters.Enemies;
using UnityEngine;

namespace Siberian25.Game.World
{
    public class EnemiesFactory
    {
        private readonly GameRoom _room;
        private readonly List<Transform> _busyPortalPoints;
        
        public EnemiesFactory(GameRoom room)
        {
            _room = room;
            _busyPortalPoints = new();
        }

        public void Spawn(float distanceFromPlayer, EnemyComposition enemyPrefab)
        {
            var points = _room.Composition.WorldGrid.Where(t =>
            {
                if (_busyPortalPoints.Contains(t))
                    return false;
                return Vector2.Distance(t.position, GameContext.Player.transform.position) > distanceFromPlayer;
            }).ToArray();
            if (points.Length <= 0)
                return;

            Spawn(points[Random.Range(0, points.Length)], enemyPrefab).Forget();
        }

        private async UniTaskVoid Spawn(Transform point, EnemyComposition enemyPrefab)
        {
            if (GameContext.CancellationToken.IsCancellationRequested)
                return;
            
            _busyPortalPoints.Add(point);
            var portal = GameContext.EnemiesPortalsPool.Get();
            portal.gameObject.SetActive(true);
            await portal.Activate();
            
            if (GameContext.CancellationToken.IsCancellationRequested)
                return;
            
            var enemy = Object.Instantiate(enemyPrefab, portal.transform.position, Quaternion.identity);
            _room.RegisterEnemy(enemy);
            enemy.AppearTween.Play();
            await enemy.AppearTween.WaitWhilePlay();
            
            if (GameContext.CancellationToken.IsCancellationRequested)
                return;
            
            await portal.Deactivate();
            
            if (GameContext.CancellationToken.IsCancellationRequested)
                return;
            
            portal.gameObject.SetActive(false);
            _busyPortalPoints.Remove(point);
        }
    }
}