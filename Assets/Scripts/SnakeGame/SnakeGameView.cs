using System.Collections.Generic;
using UnityEngine;

namespace CodeLearn.SnakeGame
{
    public class SnakeGameView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform apple;
        [SerializeField] private SnakeBody snakeBody;
        [SerializeField] private GameGrid grid;
        
        [Header("Initial Setup")]
        [SerializeField] private Vector2Int appleStartPosition;
        [SerializeField] private Vector2Int snakeStartPosition;
        [SerializeField] private int snakeSize;
        [SerializeField] private Vector3 centralizeOnGrid;
        [SerializeField] private SnakeBehaviors snakeBehaviors;

        public SnakeGame SnakeTheGame { get; private set; }
        
        private readonly List<Vector2Int> _snakePosition = new();

        private void Awake()
        {
            InitializeSnakeGame();
            UpdateSnakePosition();
            UpdateApplePosition();
        }
        
        public void Tick()
        {
            SnakeTheGame.Tick();
            UpdateSnakePosition();
            UpdateApplePosition();
        }

        public void ResetGame()
        {
            _snakePosition.Clear();
            
            for (int i = 0; i < snakeSize - 1; i++)
                _snakePosition.Add(snakeStartPosition - Vector2Int.right * i - Vector2Int.one);

            SnakeTheGame.Direction = Vector2Int.zero;
            SnakeTheGame.SetApplePosition(appleStartPosition - Vector2Int.one);
            UpdateSnakePosition();
            UpdateApplePosition();
        }

        private void InitializeSnakeGame()
        {
            for (int i = 0; i < snakeSize - 1; i++)
                _snakePosition.Add(snakeStartPosition - new Vector2Int(i, 0) - Vector2Int.one);

            SnakeTheGame = new SnakeGame(_snakePosition, appleStartPosition - Vector2Int.one, grid.GridSize);
            SnakeTheGame.SetSnakeBehavior(snakeBehaviors);
        }
        
        private void UpdateApplePosition()
        {
            apple.position = grid.ConvertIndexToWorldPosition(SnakeTheGame.Apple);
        }

        private void UpdateSnakePosition()
        {
            snakeBody.SnakeBodyLine.positionCount = SnakeTheGame.Snake.Count * 2 - 1;

            for (int i = 0; i < SnakeTheGame.Snake.Count; i++)
            {
                Vector3 bodyPosition = grid.ConvertIndexToWorldPosition(SnakeTheGame.Snake[i]);
                Vector3 currentPoint = bodyPosition + centralizeOnGrid;
                snakeBody.SnakeBodyLine.SetPosition(i * 2, bodyPosition + centralizeOnGrid);

                if(i != SnakeTheGame.Snake.Count - 1)
                {
                    Vector3 nextBodyPosition = grid.ConvertIndexToWorldPosition(SnakeTheGame.Snake[i + 1]);
                    Vector3 nextPoint = nextBodyPosition + centralizeOnGrid;
                    Vector3 smoothPoint = Vector3.Lerp(currentPoint, nextPoint, 0.5f);
                    snakeBody.SnakeBodyLine.SetPosition(i * 2 + 1, smoothPoint);
                }
            }
        }
    }
}
