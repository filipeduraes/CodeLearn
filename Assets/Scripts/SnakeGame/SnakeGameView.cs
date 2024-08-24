using System.Collections.Generic;
using UnityEngine;

namespace CodeLearn.SnakeGame
{
    public class SnakeGameView : MonoBehaviour
    {
        public SnakeGame SnakeTheGame { get; set; }

        [SerializeField] private Transform apple;
        [SerializeField] private SnakeBody snakeBody;
        [SerializeField] private GameGrid grid;
        [SerializeField] private Vector2Int appleStartPosition;
        [SerializeField] private Vector2Int snakeStartPosition;
        [SerializeField] private int snakeSize;
        [SerializeField] private Vector3 centralizeOnGrid;
        [SerializeField] private SnakeBehaviors snakeBehaviors;

        private List<Vector2Int> snakePosition = new List<Vector2Int>();

        private void Awake()
        {
            InstantiateNewSnakeGame();
        }

        private void Start()
        {
            UpdateSnakePosition();
        }

        private void Update()
        {
            UpdateSnakePosition();
            UpdateApplePosition();
        }

        private void OnEnable()
        {
            SnakeTheGame.OnSnakeGrow += GrowSnake;
        }

        private void OnDisable()
        {
            SnakeTheGame.OnSnakeGrow -= GrowSnake;
        }

        private void InstantiateNewSnakeGame()
        {
            for (int i = 0; i < snakeSize - 1; i++)
            {
                snakePosition.Add(snakeStartPosition - new Vector2Int(i, 0) - Vector2Int.one);
            }

            SnakeTheGame = new SnakeGame(snakePosition, appleStartPosition - Vector2Int.one, grid.GridSize);
            SnakeTheGame.SetSnakeBehavior(snakeBehaviors);
        }

        private void GrowSnake()
        {
            snakePosition.Add(snakePosition[snakePosition.Count - 1]);
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
