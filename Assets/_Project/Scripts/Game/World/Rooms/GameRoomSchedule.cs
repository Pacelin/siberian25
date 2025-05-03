using UnityEngine;

namespace Siberian25.Game.World
{
    [CreateAssetMenu(menuName = "Game/Room Schedule")]
    public class GameRoomSchedule : ScriptableObject
    {
        [SerializeField] private GameRoomStage[] _stages;
    }
}