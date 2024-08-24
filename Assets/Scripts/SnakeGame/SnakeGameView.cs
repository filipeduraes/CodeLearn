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

        private List<LineRenderer> _snakeLineRendererBody = new();

        private void Awake()
        {
            InitializeSnakeGame();
            UpdateSnakePosition();
            UpdateApplePosition();
        }

        private void OnEnable()
        {
            SnakeTheGame.OnSnakeGrow += snakeBody.GrowSnakeBody;
        }

        private void OnDisable()
        {
            SnakeTheGame.OnSnakeGrow -= snakeBody.GrowSnakeBody;
        }

        public void Tick()
        {
            SnakeTheGame.Tick();
            UpdateSnakePosition();
            UpdateApplePosition();
            //snakeBody.UpdateSnakeBody();
        }

        public void ResetGame()
        {
            snakeBody.ResetBody();
            _snakePosition.Clear();
            InitializeBody();

            SnakeTheGame.Direction = Vector2Int.zero;
            SnakeTheGame.SetApplePosition(appleStartPosition - Vector2Int.one);
            UpdateSnakePosition();
            UpdateApplePosition();
        }

        private void InitializeSnakeGame()
        {
            InitializeBody();

            SnakeTheGame = new SnakeGame(_snakePosition, appleStartPosition - Vector2Int.one, grid.GridSize);
            SnakeTheGame.SetSnakeBehavior(snakeBehaviors);
        }

        private void InitializeBody()
        {
            List<Vector3> bodyPositions = new();
            for (int i = 0; i < snakeSize - 1; i++)
            {
                _snakePosition.Add(snakeStartPosition - Vector2Int.right * i - Vector2Int.one);
                bodyPositions.Add(grid.ConvertIndexToWorldPosition(_snakePosition[i]));
            }

            snakeBody.InstantiateBody(bodyPositions);
        }

        private void UpdateApplePosition()
        {
            apple.position = grid.ConvertIndexToWorldPosition(SnakeTheGame.Apple);
        }

        private void UpdateSnakePosition()
        {
            for (int i = 0; i < SnakeTheGame.Snake.Count - 1; i++)
            {
                Vector3 bodyPosition = grid.ConvertIndexToWorldPosition(SnakeTheGame.Snake[i]);
                Vector3 nextBodyPosition = grid.ConvertIndexToWorldPosition(SnakeTheGame.Snake[i + 1]);
                bodyPosition += centralizeOnGrid;
                nextBodyPosition += centralizeOnGrid;

                snakeBody.UpdateBodyLine(i, bodyPosition, nextBodyPosition);
            }
        }
    }
}
