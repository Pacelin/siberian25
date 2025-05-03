using UnityEngine;

namespace Siberian25.Game.World
{
    public class GameWorldComposition : MonoBehaviour
    {
        public GameRoomComposition FirstRoomPrefab => _firstRoomPrefab;
        public PortalView PlayerPortalPrefab => _playerPortalPrefab;
        public PortalView EnemyPortalPrefab => _enemyPortalPrefab;
        public FinishPortalView FinishPortalPrefab => _finishPortalPrefab;

        public GameWorldStateMachine StateMachine => _stateMachine;
        
        [SerializeField] private GameRoomComposition _firstRoomPrefab;
        [SerializeField] private PortalView _playerPortalPrefab;
        [SerializeField] private PortalView _enemyPortalPrefab;
        [SerializeField] private FinishPortalView _finishPortalPrefab;

        private GameWorldStateMachine _stateMachine;
        
        private void Start()
        {
            _stateMachine = new GameWorldStateMachine();
            _stateMachine.Run();
        }

        private void OnDestroy()
        {
            _stateMachine.Stop();
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }
}