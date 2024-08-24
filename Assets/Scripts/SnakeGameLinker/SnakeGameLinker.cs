using System;
using CodeLearn.CodeEditor;
using CodeLearn.Minigame;
using UnityEngine;

namespace CodeLearn.SnakeGame.Linker
{
    public class SnakeGameLinker
    {
        private readonly SnakeGame _snakeGame;
        private readonly SnakeGameInputModule _inputModule;
        private readonly MemoryModule _memoryModule;

        private const string HorizontalInputKey = "InputX";
        private const string VerticalInputKey = "InputY";
        private const string CollidingAppleKey = "ColidindoComFruta";
        private const string DirectionX = "DirecaoX";
        private const string DirectionY = "DirecaoY";
        private const string SnakeGrowFunction = "CrescerCobra";
        private const string RandomizeAppleFunction = "RandomizarFruta";
        
        public SnakeGameLinker(SnakeGame snakeGame, SnakeGameInputModule inputModule, MemoryModule memoryModule)
        {
            _snakeGame = snakeGame;
            _inputModule = inputModule;
            _memoryModule = memoryModule;
            
            VariableLinker linker = new(memoryModule);
            
            linker.SubscribeSignalReceiver(DirectionX, UpdateDirectionX);
            linker.SubscribeSignalReceiver(DirectionY, UpdateDirectionY);
            
            memoryModule.StoreValue(RandomizeAppleFunction, new Action(snakeGame.RandomizeApple));
            memoryModule.StoreValue(SnakeGrowFunction, new Action(snakeGame.GrowSnake));
            memoryModule.StoreValue(CollidingAppleKey, false);
            memoryModule.StoreValue(HorizontalInputKey, 0.0f);
            memoryModule.StoreValue(VerticalInputKey, 0.0f);
            
            _snakeGame.CollidingApple.OnValueChanged += UpdateCollidingApple;
            _inputModule.HorizontalAxis.OnValueChanged += UpdateHorizontalInput;
            _inputModule.VerticalAxis.OnValueChanged += UpdateVerticalInput;
        }

        ~SnakeGameLinker()
        {
            _snakeGame.CollidingApple.OnValueChanged -= UpdateCollidingApple;
            _inputModule.HorizontalAxis.OnValueChanged -= UpdateHorizontalInput;
            _inputModule.VerticalAxis.OnValueChanged -= UpdateVerticalInput;
        }

        private void UpdateDirectionX(object value)
        {
            if (value is float number)
            {
                int xDirection = Mathf.RoundToInt(number);
                SetDirection(xDirection, _snakeGame.Direction.y);
            }
        }
        
        private void UpdateDirectionY(object value)
        {
            if (value is float number)
            {
                int yDirection = Mathf.RoundToInt(number);
                SetDirection(_snakeGame.Direction.x, yDirection);
            }
        }

        private void SetDirection(int x, int y)
        {
            _snakeGame.Direction = new Vector2Int(x, y);
        }
        
        private void UpdateCollidingApple(bool newValue)
        {
            _memoryModule.StoreValue(CollidingAppleKey, newValue);
        }
        
        private void UpdateHorizontalInput(float newInputX)
        {
            _memoryModule.StoreValue(HorizontalInputKey, newInputX);
        }
        
        private void UpdateVerticalInput(float newInputY)
        {
            _memoryModule.StoreValue(VerticalInputKey, newInputY);
        }
    }
}