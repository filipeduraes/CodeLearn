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

        public NotifiedProperty<bool> CollidingApple { get; private set; }

        public event Action OnSnakeCollision = delegate { };
        public event Action OnSnakeGrow = delegate { };

        private int _gridSize;
        private SnakeBehaviors _snakeBehaviors;

        public SnakeGame(List<Vector2Int> snakePositions,Vector2Int applePosition, int gridSize)
        {
            Snake = snakePositions;
            Apple = applePosition;
            _gridSize = gridSize;
        }

        public void Tick()
        {
            Vector2Int newHead = Snake[0] + Direction;

            CheckCollisions(newHead);

            for (int i = Snake.Count - 2; i >= 0 ; i--)
            {
                Snake[i + 1] = Snake[i];
            }

            Snake[0] = newHead;
        }
        public void RandomizeApple()
        {
            if ((_snakeBehaviors & SnakeBehaviors.RandomizeApple) != 0)
            {
                int newAppleX = Random.Range(0, _gridSize);
                int newAppleY = Random.Range(0, _gridSize);
                Vector2Int newApple = new Vector2Int(newAppleX, newAppleY);

                if (Snake.Contains(newApple))
                {
                    RandomizeApple();
                    return;
                }

                Apple = newApple;
            }
        }
        public void SetSnakeBehavior(SnakeBehaviors behavior)
        {
            _snakeBehaviors = behavior;
        }

        private void CheckCollisions(Vector2Int headPosition)
        {
            CollidingApple.Set(headPosition == Apple);

            if (headPosition == Apple && (_snakeBehaviors & SnakeBehaviors.EatApple) != 0)
            {
                GrowSnake();
                RandomizeApple();
            }

            if (Snake.Contains(headPosition) || IsOutsideBoard(headPosition) && (_snakeBehaviors & SnakeBehaviors.Collide) != 0)
            {
                OnSnakeCollision();
            }
        }

        private bool IsOutsideBoard(Vector2Int headPosition)
        {
            return headPosition.x < 0 || headPosition.y < 0 || headPosition.x >= _gridSize || headPosition.y >= _gridSize;
        }

        private void GrowSnake()
        {
            if ((_snakeBehaviors & SnakeBehaviors.Grow) != 0)
            {
                Snake.Add(Snake[Snake.Count - 1]);
                OnSnakeGrow();
            }
        }
    }
}