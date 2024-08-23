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

        public void UpdateSnakeBody()
        {
            Vector3 headPosition = SnakeBodyLine.GetPosition(1);
            Vector3 tailPosition = SnakeBodyLine.GetPosition(SnakeBodyLine.positionCount - 2);

            head.position = headPosition;
            tail.position = tailPosition;

            RotateSnakeBody();
        }

        private void RotateSnakeBody()
        {
            Vector3 directionHead = (snakeBodyLine.GetPosition(0) - SnakeBodyLine.GetPosition(1)).normalized;
            head.rotation = Quaternion.FromToRotation(head.right, directionHead) * head.rotation;

            Vector3 directionTail = (SnakeBodyLine.GetPosition(SnakeBodyLine.positionCount - 2) - SnakeBodyLine.GetPosition(SnakeBodyLine.positionCount - 1)).normalized;
            tail.rotation = Quaternion.FromToRotation(tail.right, directionTail) * tail.rotation;

            Vector2 textureScale = snakeBodyLine.textureScale;
            textureScale.y = 1;

            if (Vector3.Dot(head.up, Vector3.up) < 0)
            {
                head.rotation = Quaternion.FromToRotation(head.up, - head.up) * head.rotation;
                tail.rotation = Quaternion.FromToRotation(tail.up, -tail.up) * tail.rotation;
                textureScale.y = -1;
            }
            SnakeBodyLine.textureScale = textureScale;
        }
    }
}