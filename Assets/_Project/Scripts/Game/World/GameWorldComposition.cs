using Cysharp.Threading.Tasks;
using TSS.Tweening;
using UnityEngine;

namespace Siberian25.Game.World
{
    public class GameWorldComposition : MonoBehaviour
    {
        public GameRoomComposition FirstRoomPrefab => _firstRoomPrefab;
        public PortalView EnemyPortalPrefab => _enemyPortalPrefab;

        public GameWorldStateMachine StateMachine => _stateMachine;

        [SerializeField] private ScriptableTween _fadeInTween;
        [SerializeField] private ScriptableTween _fadeOutTween;
        [SerializeField] private GameRoomComposition _firstRoomPrefab;
        [SerializeField] private PortalView _enemyPortalPrefab;

        private GameWorldStateMachine _stateMachine;

        public async UniTask FadeInWorld()
        {
            _fadeInTween.Play();
            await _fadeInTween.WaitWhilePlay();
        }
        public async UniTask FadeOutWorld()
        {
            _fadeOutTween.Play();
            await _fadeOutTween.WaitWhilePlay();
        }
        
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