using System.Collections.Generic;
using UnityEngine;

namespace Siberian25.Game.World
{
    public class GameWorldRoomBattleState : GameWorldState
    {
        private readonly Queue<GameRoomStageItem> _itemsQueue = new();
        private int _roomStageIndex = 0;
        private float _time;
        private float _lastTime;
        
        public override void OnEnter()
        {
            foreach (var item in GameContext.ActiveRoom.Schedule.Stages[0].Items)
                _itemsQueue.Enqueue(item);
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate()
        {
            _time += Time.deltaTime;

            if (_itemsQueue.Count == 0)
            {
                if (_roomStageIndex >= GameContext.ActiveRoom.Schedule.Stages.Count - 1)
                    return;
                if (GameContext.ActiveRoom.EnemiesExists)
                    return;
                
                _time = 0;
                foreach (var item in GameContext.ActiveRoom.Schedule.Stages[++_roomStageIndex].Items)
                    _itemsQueue.Enqueue(item);
            }
            else
            {
                var currentItem = _itemsQueue.Peek();
                if (_time >= currentItem.Time &&
                    _lastTime <= currentItem.Time)
                {
                    _itemsQueue.Dequeue();
                    currentItem.Execute();
                }
            }
            
            _lastTime = _time;
        }
    }
}