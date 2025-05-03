using System.Collections.Generic;
using UnityEngine;

namespace Siberian25.Game.World
{
    [CreateAssetMenu(menuName = "Game/Room Schedule")]
    public class GameRoomSchedule : ScriptableObject
    {
        public IReadOnlyList<GameRoomStage> Stages => _stages;
        
        [SerializeField] private GameRoomStage[] _stages;
    }
}