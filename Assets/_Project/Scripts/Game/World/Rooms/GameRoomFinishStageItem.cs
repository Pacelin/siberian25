namespace Siberian25.Game.World
{
    public class GameRoomFinishStageItem : GameRoomStageItem
    {
        public override bool IsFinished() => false;

        public override void OnStart()
        {
            // Spawn Finish Portal
        }

        public override void OnFinish()
        {
            
        }
    }
}