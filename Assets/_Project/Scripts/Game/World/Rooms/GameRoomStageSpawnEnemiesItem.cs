using Siberian25.Game.Characters.Enemies;
using UnityEngine;

namespace Siberian25.Game.World
{
    [CreateAssetMenu(menuName = "Game/Schedule/Spawn Enemies")]
    public class GameRoomStageSpawnEnemiesItem : GameRoomStageItem
    {
        [SerializeField] private float _distanceFromPlayer;
        [SerializeField] private EnemyComposition[] _enemies;

        public override void Execute()
        {
            foreach (var enemy in _enemies)
                GameContext.ActiveRoom.EnemiesFactory.Spawn(_distanceFromPlayer, enemy);
        }
    }
}