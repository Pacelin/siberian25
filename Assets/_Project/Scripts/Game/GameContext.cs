using System.Threading;
using Siberian25.Game.Characters;
using Siberian25.Game.World;
using UnityEngine.Pool;

namespace Siberian25.Game
{
    [System.Serializable]
    public static class GameContext
    {
        public static CancellationToken CancellationToken { get; set; }
        
        public static PlayerComposition Player { get; set; }
        public static ObjectPool<PortalView> EnemiesPortalsPool { get; set; }
        public static GameWorldComposition World { get; set; }
        public static GameRoom ActiveRoom { get; set; }
    }
}