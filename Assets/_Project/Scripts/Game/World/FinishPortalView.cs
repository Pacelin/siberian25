using System.Collections.Generic;
using UnityEngine;

namespace Siberian25.Game.World
{
    public class FinishPortalView : PortalView
    {
        public IReadOnlyList<GameRoomComposition> Rooms => _rooms;

        [SerializeField] private GameRoomComposition[] _rooms;
    }
}