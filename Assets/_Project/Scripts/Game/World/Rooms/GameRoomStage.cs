using System.Collections.Generic;
using UnityEngine;

namespace Siberian25.Game.World
{
    [CreateAssetMenu(menuName = "Game/Room Stage")]
    public class GameRoomStage : ScriptableObject
    {
        public IReadOnlyList<GameRoomStageItem> Items => _items;

        [SerializeField] private GameRoomStageItem[] _items;
    }
}