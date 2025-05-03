using System;
using UnityEngine;

namespace Siberian25.Game.Characters
{
    public class PlayerComposition : CharacterComposition
    {
        public PlayerInput Input => _input;
        public TrailRenderer DashTrail => _dashTrail;
        public PlayerCharacterConfig Config => _config;
        public PlayerSliceTrigger SliceTrigger => _sliceTrigger;

        public PlayerStateMachine StateMachine => _stateMachine;

        [Space]
        [SerializeField] private PlayerInput _input;
        [SerializeField] private TrailRenderer _dashTrail;
        [SerializeField] private PlayerSliceTrigger _sliceTrigger;
        [SerializeField] private PlayerCharacterConfig _config;
        
        private PlayerStateMachine _stateMachine;

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
            _stateMachine.FixedUpdate();
        }
    }
}