using UnityEngine;

namespace Siberian25.Game.Characters
{
    public class PlayerComposition : CharacterComposition
    {
        public PlayerCombo Combo => _combo;
        public PlayerArrow Arrow => _arrow;
        public PlayerInput Input => _input;
        public TrailRenderer DashTrail => _dashTrail;
        public PlayerCharacterConfig Config => _config;
        public PlayerSliceTrigger SliceTrigger => _sliceTrigger;

        public PlayerStateMachine StateMachine => _stateMachine;
        public bool Collision { get; set; }
        
        [Space] 
        [SerializeField] private PlayerCombo _combo;
        [SerializeField] private PlayerArrow _arrow;
        [SerializeField] private PlayerInput _input;
        [SerializeField] private TrailRenderer _dashTrail;
        [SerializeField] private PlayerSliceTrigger _sliceTrigger;
        [SerializeField] private PlayerCharacterConfig _config;
        
        private PlayerStateMachine _stateMachine;
        private float _dashCooldownExtimation;
        
        public void StartDashCooldown() => _dashCooldownExtimation = Time.unscaledTime + _config.DashCooldown;
        public bool CanDash() => Time.unscaledTime > _dashCooldownExtimation;
        
        private void Awake()
        {
            _stateMachine = new PlayerStateMachine();
            _dashTrail.emitting = false;
        }

        private void Start()
        {
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

        private void FixedUpdate()
        {
            if (!Health || Health.IsDead)
                return;
            _stateMachine.FixedUpdate();
        }

        private void OnCollisionEnter2D(Collision2D col) => Collision = true;
        private void OnCollisionStay2D(Collision2D col) => Collision = true;
    }
}