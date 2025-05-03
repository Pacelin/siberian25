using System.Collections.Generic;
using UnityEngine;

namespace Siberian25.Game.World
{
    public class GameRoomComposition : MonoBehaviour
    {
        public Transform PlayerPoint => _playerPoint;
        public Transform FinishPoint => _finishPoint;
        public IReadOnlyList<Transform> WorldGrid => _worldGrid;
        public IReadOnlyList<GameRoomSchedule> PossibleSchedules => _possibleSchedules;
        
        [SerializeField] private Transform _playerPoint;
        [SerializeField] private Transform _finishPoint;
        [SerializeField] private Transform[] _worldGrid;
        [SerializeField] private GameRoomSchedule[] _possibleSchedules;
    }
}