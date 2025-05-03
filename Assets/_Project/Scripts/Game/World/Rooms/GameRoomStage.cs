using UnityEngine;

namespace Siberian25.Game.World
{
    [CreateAssetMenu(menuName = "Game/Room Stage")]
    public class GameRoomStage : ScriptableObject
    {
        [SerializeField] private GameRoomStageItem[] _items;
    }
}