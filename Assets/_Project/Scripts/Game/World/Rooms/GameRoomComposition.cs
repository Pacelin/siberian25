using System.Collections.Generic;
using UnityEngine;

namespace Siberian25.Game.World
{
    public class GameRoomComposition : MonoBehaviour
    {
        public PortalView PlayerPortal => _playerPortal;
        public FinishPortalView FinishPortal => _finishPortal;
        public IReadOnlyList<Transform> WorldGrid => _worldGrid;
        public IReadOnlyList<GameRoomSchedule> PossibleSchedules => _possibleSchedules;

        [SerializeField] private PortalView _playerPortal;
        [SerializeField] private FinishPortalView _finishPortal;
        [SerializeField] private Transform[] _worldGrid;
        [SerializeField] private GameRoomSchedule[] _possibleSchedules;
    }
}