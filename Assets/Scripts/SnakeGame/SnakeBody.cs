using UnityEngine;

namespace CodeLearn.SnakeGame
{
    public class SnakeBody : MonoBehaviour
    {
        [SerializeField] private LineRenderer snakeBodyLine;
        [SerializeField] private Transform head;
        [SerializeField] private Transform tail;

        public LineRenderer SnakeBodyLine => snakeBodyLine;

        private void Update()
        {
            UpdateSnakeBody();
        }
        private void OnEnable()
        {
            SnakeGame.OnSnakeGrow += UpdateSnakeGrow;
        }

        private void OnDisable()
        {
            SnakeGame.OnSnakeGrow -= UpdateSnakeGrow;
        }

        public void UpdateSnakeBody()
        {
            Vector3 headPosition = snakeBodyLine.GetPosition(0);
            Vector3 tailPosition = snakeBodyLine.GetPosition(snakeBodyLine.positionCount - 1);

            head.position = headPosition;
            tail.position = tailPosition;


            Vector3 directionHead = (head.position - snakeBodyLine.GetPosition(1)).normalized;
            head.rotation = Quaternion.FromToRotation(head.right, directionHead) * head.rotation;

            Vector3 directionTail = (snakeBodyLine.GetPosition(snakeBodyLine.positionCount - 1) - tail.position).normalized;
            tail.rotation = Quaternion.FromToRotation(tail.right, directionTail) * tail.rotation;
        }

        public void UpdateSnakeGrow()
        {
            snakeBodyLine.positionCount++;
        }
    }
}