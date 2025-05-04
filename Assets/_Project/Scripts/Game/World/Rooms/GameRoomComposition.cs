using System.Collections.Generic;
using TSS.Audio;
using UnityEngine;

namespace Siberian25.Game.World
{
    public class GameRoomComposition : MonoBehaviour
    {
        public int Track => _track;
        public PortalView PlayerPortal => _playerPortal;
        public SoundEvent StepSound => _stepSound;
        public FinishPortalView FinishPortal => _finishPortal;
        public IReadOnlyList<Transform> WorldGrid => _worldGrid;
        public IReadOnlyList<GameRoomSchedule> PossibleSchedules => _possibleSchedules;

        [SerializeField] private int _track = 0;
        [SerializeField] private SoundEvent _stepSound;
        [SerializeField] private PortalView _playerPortal;
        [SerializeField] private FinishPortalView _finishPortal;
        [SerializeField] private Transform[] _worldGrid;
        [SerializeField] private GameRoomSchedule[] _possibleSchedules;
    }
}