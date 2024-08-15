using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeLearn.SnakeGame
{
    [Flags]
    public enum SnakeBehaviors
    {
        Move = 1,
        EatApple = 2,
        Grow = 4,
        Collide = 8,
    }



    public class SnakeGame
    {
        public Vector2Int Direction { get; set; }
        private Vector2Int _apple;
        private List<Vector2Int> _snake;
        private int _gridSize;

        public delegate void SnakeColliding();
        public static event SnakeColliding OnSnakeCollision = delegate { };

        public SnakeGame(List<Vector2Int> snakePositions,Vector2Int applePosition, int gridSize)
        {
            _snake = snakePositions;
            _apple = applePosition;
            _gridSize = gridSize;
        }

        public void Tick()
        {
            Vector2Int newHead = _snake[0] + Direction;

            CheckCollisions(newHead);

            for (int i = _snake.Count - 2; i >= 0 ; i--)
            {
                _snake[i + 1] = _snake[i];
            }

            _snake[0] = newHead;
        }

        private void CheckCollisions(Vector2Int headPosition)
        {
            if (headPosition == _apple)
            {
                GrowSnake();
                RandomizeApple();
            }

            if (_snake.Contains(headPosition) || IsOutsideBoard(headPosition))
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
            _snake.Add(_snake[_snake.Count - 1]);
        }

        private void RandomizeApple()
        {
            int newAppleX = Random.Range(0, _gridSize);
            int newAppleY = Random.Range(0, _gridSize);
            Vector2Int newApple = new Vector2Int(newAppleX, newAppleY);

            if (_snake.Contains(newApple))
            {
                RandomizeApple();
                return;
            }

            _apple = newApple;
        }




    }
}