using System;
using CodeLearn.CodeEditor;
using CodeLearn.Minigame;
using UnityEngine;

namespace CodeLearn.SnakeGame.Linker
{
    public class SnakeGameLinker
    {
        private readonly SnakeGame _snakeGame;
        private readonly MemoryModule _memoryModule;

        private const string CollidingAppleKey = "ColidindoComFruta";
        private const string DirectionX = "DirecaoX";
        private const string DirectionY = "DirecaoY";
        private const string RandomizeAppleFunction = "RandomizarFruta";
        
        public SnakeGameLinker(SnakeGame snakeGame, MemoryModule memoryModule)
        {
            _snakeGame = snakeGame;
            _memoryModule = memoryModule;
            VariableLinker linker = new(memoryModule);
            
            linker.SubscribeSignalReceiver(DirectionX, UpdateDirectionX);
            linker.SubscribeSignalReceiver(DirectionY, UpdateDirectionY);
            memoryModule.StoreValue(RandomizeAppleFunction, new Action(snakeGame.RandomizeApple));
            memoryModule.StoreValue(CollidingAppleKey, false);
            
            _snakeGame.CollidingApple.OnValueChanged += UpdateCollidingApple;
        }

        ~SnakeGameLinker()
        {
            _snakeGame.CollidingApple.OnValueChanged -= UpdateCollidingApple;
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
    }
}