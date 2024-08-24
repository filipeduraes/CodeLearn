using System;
using CodeLearn.Input;
using CodeLearn.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeLearn.SnakeGame
{
    public class SnakeGameInputModule : MonoBehaviour
    {
        public NotifiedProperty<float> HorizontalAxis { get; } = new();
        public NotifiedProperty<float> VerticalAxis { get; } = new();
        
        private PlayerAction _playerInputs;

        private void Awake()
        {
            _playerInputs = new PlayerAction();
        }

        private void OnEnable()
        {
            _playerInputs.Enable();
            _playerInputs.Player.Axis.started += UpdateVariables;
        }

        private void OnDisable()
        {
            _playerInputs.Player.Axis.started -= UpdateVariables;
            _playerInputs.Disable();
        }

        private void OnDestroy()
        {
            _playerInputs.Dispose();
        }
        
        private void UpdateVariables(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            HorizontalAxis.Set(input.x);
            VerticalAxis.Set(input.y);
        }
    }
}