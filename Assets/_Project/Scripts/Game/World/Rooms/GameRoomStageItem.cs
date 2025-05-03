using UnityEngine;

namespace Siberian25.Game.World
{
    public abstract class GameRoomStageItem : ScriptableObject
    {
        public float Time => _time;
        
        [SerializeField] private float _time;

        public abstract void Execute();
    }
}