using Siberian25.Game.Characters.Enemies;
using UnityEngine;

namespace Siberian25.Game.Characters.Boss
{
    [System.Serializable]
    public struct PhaseSettings
    {
        public float LongAttackChance;
        public float ShortAttackChance;
        public float LaserAttackChance;
        public float BulletAttackChance;
        public float SpawnEnemiesChance;
        public Vector2 SpawnEnemiesIdleRange;
        public EnemyComposition[] PhaseEnemies;
        public EnemyComposition[] BeforePhaseEnemies;
    }
    
    public class BossComposition : MonoBehaviour
    {
        public bool LeftHeadAlive => _leftHead && _leftHead.IsAlive;
        public bool MiddleHeadAlive => _middleHead && _middleHead.IsAlive;
        public bool RightHeadAlive => _rightHead && _rightHead.IsAlive;

        public int PhaseIndex
        {
            get
            {
                if (LeftHeadAlive && RightHeadAlive)
                    return 0;
                if (LeftHeadAlive || RightHeadAlive)
                    return 1;
                return 2;
            }
        }
        public PhaseSettings CurrentPhase => _phases[PhaseIndex];

        public Animator WholeAnimator => _wholeAnimator;
        public BossHead LeftHead => _leftHead;
        public BossHead MiddleHead => _middleHead;
        public BossHead RightHead => _rightHead;

        public Vector2 IdleDurationRange => _idleDurationRange;

        public float ShortAttackBothHeadsChance => _bothHeadsChance;

        [SerializeField] private Animator _wholeAnimator;
        [SerializeField] private BossHead _leftHead;
        [SerializeField] private BossHead _middleHead;
        [SerializeField] private BossHead _rightHead;
        [SerializeField] private PhaseSettings[] _phases;

        [Header("Idle Settings")] 
        [SerializeField] private Vector2 _idleDurationRange;
        [Header("Short Attack Settings")]
        [SerializeField] private float _bothHeadsChance;
        
        private BossStateMachine _stateMachine;

        private void Awake()
        {
            GameContext.Boss = this;
        }

        private void Start()
        {
            _stateMachine = new BossStateMachine(this);
            _stateMachine.Run();
        }

        private void OnDestroy() => _stateMachine.Stop();
        private void Update() => _stateMachine.Update();

        public void SwitchStateToAttack()
        {
            var r = Random.Range(0f, 1f);
            if (r < CurrentPhase.ShortAttackChance)
                _stateMachine.SwitchState(new BossShortAttackState());
            else if (r < CurrentPhase.ShortAttackChance + CurrentPhase.LongAttackChance)
                _stateMachine.SwitchState(new BossLongAttackState());
            else if (r < CurrentPhase.ShortAttackChance + CurrentPhase.LongAttackChance + CurrentPhase.LaserAttackChance)
                _stateMachine.SwitchState(new BossLaserAttackState());
            else if (r < CurrentPhase.ShortAttackChance + CurrentPhase.LongAttackChance + CurrentPhase.LaserAttackChance + CurrentPhase.BulletAttackChance)
                _stateMachine.SwitchState(new BossBulletsAttackState());
            else
                _stateMachine.SwitchState(new BossShortAttackState());
        }
        
        public void SwitchStateToSpawnEnemies()
        {
            if (Random.Range(0f, 1f) <= CurrentPhase.SpawnEnemiesChance)
                _stateMachine.SwitchState(new BossSpawnEnemiesState());
            else
                _stateMachine.SwitchState(new BossIdleState());
        }
    }
}