using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Siberian25.Game.Characters
{
    public class PlayerInput : MonoBehaviour
    {
        public bool IsMove => MoveInput != Vector2.zero;
        public bool IsIdle => MoveInput == Vector2.zero;
        
        public bool IsDashing => _dashInput.WasPerformedThisFrame() && !EventSystem.current.IsPointerOverGameObject();
        public Vector2 MoveInput => _moveInput.ReadValue<Vector2>();
        
        [SerializeField] private InputAction _moveInput;
        [SerializeField] private InputAction _dashInput;

        private void OnEnable()
        {
            _moveInput.Enable();
            _dashInput.Enable();
        }

        private void OnDisable()
        {
            _moveInput.Disable();
            _dashInput.Disable();
        }
    }
}