using System.Collections;
using UnityEngine;

namespace CodeLearn.SnakeGame
{
    public class TestRun : MonoBehaviour
    {
        [SerializeField] private SnakeGameView snakeGameView;
        [SerializeField] private float delaySeconds;

        private void Start()
        {
            StartCoroutine(SlowUpdate());
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && snakeGameView.SnakeTheGame.Direction != Vector2Int.down)
            {
                snakeGameView.SnakeTheGame.Direction = Vector2Int.up;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && snakeGameView.SnakeTheGame.Direction != Vector2Int.up)
            {
                snakeGameView.SnakeTheGame.Direction = Vector2Int.down;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && snakeGameView.SnakeTheGame.Direction != Vector2Int.right)
            {
                snakeGameView.SnakeTheGame.Direction = Vector2Int.left;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && snakeGameView.SnakeTheGame.Direction != Vector2Int.left)
            {
                snakeGameView.SnakeTheGame.Direction = Vector2Int.right;
            }
        }
         
        IEnumerator SlowUpdate()
        {
            while (true)
            {
                yield return new WaitForSeconds(delaySeconds);
                snakeGameView.SnakeTheGame.Tick();
            }
        }

    }
}
