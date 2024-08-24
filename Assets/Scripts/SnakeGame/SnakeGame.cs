using System;
using System.Collections.Generic;
using CodeLearn.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeLearn.SnakeGame
{
    public class SnakeGame
    {
        public Vector2Int Direction { get; set; }
        public Vector2Int Apple { get; private set; }
        public List<Vector2Int> Snake { get; private set; }
        public NotifiedProperty<bool> CollidingApple { get; } = new();

        public event Action OnSnakeGrow = delegate { };

        private SnakeBehaviors _snakeBehaviors;
        private readonly int _gridSize;

        public SnakeGame(List<Vector2Int> snakePositions,Vector2Int applePosition, int gridSize)
        {
            Snake = snakePositions;
            Apple = applePosition;
            _gridSize = gridSize;
        }

        public void Tick()
        {
            if(Direction.sqrMagnitude == 0f)
                return;
            
            Vector2Int newHead = Snake[0] + Direction;

            CheckCollisions(newHead);

            for (int i = Snake.Count - 2; i >= 0 ; i--)
                Snake[i + 1] = Snake[i];

            Snake[0] = newHead;
        }

        public void RandomizeApple()
        {
            while (true)
            {
                int newAppleX = Random.Range(0, _gridSize);
                int newAppleY = Random.Range(0, _gridSize);

                Vector2Int newApple = new(newAppleX, newAppleY);

                if (Snake.Contains(newApple))
                {
                    continue;
                }

                Apple = newApple;
                CheckCollisions(Snake[0]);

                break;
            }
        }

        public void SetSnakeBehavior(SnakeBehaviors behavior)
        {
            _snakeBehaviors = behavior;
        }
        
        public void SetApplePosition(Vector2Int appleStartPosition)
        {
            Apple = appleStartPosition;
        }
        
        public void GrowSnake()
        {
            Snake.Add(Snake[Snake.Count - 1]);
            OnSnakeGrow();
        }

        private void CheckCollisions(Vector2Int headPosition)
        {
            CollidingApple.Set(headPosition == Apple);
        }

        private bool IsOutsideBoard(Vector2Int headPosition)
        {
            return headPosition.x < 0 || headPosition.y < 0 || headPosition.x >= _gridSize || headPosition.y >= _gridSize;
        }
    }
}