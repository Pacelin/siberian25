using System.Threading;
using Siberian25.Game.Characters;

namespace Siberian25.Game
{
    [System.Serializable]
    public static class GameContext
    {
        public static CancellationToken CancellationToken { get; set; }
        
        public static PlayerComposition Player { get; set; }
    }
}