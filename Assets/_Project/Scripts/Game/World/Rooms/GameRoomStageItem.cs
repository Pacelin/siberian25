using UnityEngine;

namespace Siberian25.Game.World
{
    public abstract class GameRoomStageItem : ScriptableObject
    {
        public abstract bool IsFinished();
        public abstract void OnStart();
        public abstract void OnFinish();
    }
}