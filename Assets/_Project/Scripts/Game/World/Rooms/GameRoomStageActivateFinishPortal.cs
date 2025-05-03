using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Siberian25.Game.World
{
    [CreateAssetMenu(menuName = "Game/Schedule/Activate Finish Portal")]
    public class GameRoomStageActivateFinishPortal : GameRoomStageItem
    {
        public override void Execute()
        {
            GameContext.ActiveRoom.Composition.FinishPortal.Activate().Forget();
        }
    }
}