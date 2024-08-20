using System.Collections.Generic;
using UnityEngine;

namespace CodeLearn.SnakeGame
{
    public class SnakeGameView : MonoBehaviour
    {
        [SerializeField] private Transform apple;
        [SerializeField] private SnakeBody snakeBody;
        [SerializeField] private GameGrid grid;
        [SerializeField] private Vector2Int appleStartPosition;
        [SerializeField] private Vector2Int snakeStartPosition;
        [SerializeField] private int snakeSize;
        [SerializeField] private Vector3 CentralizeOnGrid;

        private SnakeGame _snakeGame;
        private List<Vector2Int> snakePositionList = new List<Vector2Int>();

        private void Awake()
        {
            InstantiateNewSnakeGame();
        }

        private void Start()
        {
            SetSnakePosition();
            SetApplePosition();
        }

        private void Update()
        {
            UpdateSnakePosition();
            UpdateApplePosition();
        }

        private void InstantiateNewSnakeGame()
        {
            for (int i = 0; i <= snakeSize - 2; i++)
            {
                snakePositionList.Add(snakeStartPosition - new Vector2Int(i, 0) - new Vector2Int (1,1));
                snakeBody.UpdateSnakeGrow();
            }

            _snakeGame = new SnakeGame(snakePositionList, appleStartPosition - new Vector2Int(1, 1), grid.GridSize);
        }

        private void GrowSnake()
        {
            snakePositionList.Add(snakePositionList[snakePositionList.Count - 1]);
            _snakeGame.Snake.Add(_snakeGame.Snake[_snakeGame.Snake.Count - 1]);
        }

        private void SetApplePosition()
        {
            apple.position = grid.ConvertIndexToWorldPosition(appleStartPosition);
        }

        private void UpdateApplePosition()
        {
            apple.position = grid.ConvertIndexToWorldPosition(_snakeGame.Apple);
        }

        private void SetSnakePosition()
        {
            for (int i = 0; i < _snakeGame.Snake.Count; i++)
            {
                Vector3 bodyPosition = grid.ConvertIndexToWorldPosition(snakePositionList[i]);
                snakeBody.SnakeBodyLine.SetPosition(i, bodyPosition + CentralizeOnGrid);
            }
        }

        private void UpdateSnakePosition()
        {
            snakeBody.UpdateSnakeBody();

            for (int i = 0; i < _snakeGame.Snake.Count; i++)
            {
                Vector3 bodyPosition = grid.ConvertIndexToWorldPosition(_snakeGame.Snake[i]);
                snakeBody.SnakeBodyLine.SetPosition(i, bodyPosition + CentralizeOnGrid);
            }
        }

        private void OnEnable()
        {
            SnakeGame.OnSnakeGrow += GrowSnake;
        }

        private void OnDisable()
        {
            SnakeGame.OnSnakeGrow -= GrowSnake;
        }
    }
}
