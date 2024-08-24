using System.Collections.Generic;
using UnityEngine;

namespace CodeLearn.SnakeGame
{
    public class SnakeBody : MonoBehaviour
    {
        [SerializeField] private LineRenderer snakeBodyLine;
        [SerializeField] private Transform head;
        [SerializeField] private Transform tail;

        private List<LineRenderer> _snakeLineRendererBody = new();
        public LineRenderer SnakeBodyLine => snakeBodyLine;

        public void UpdateSnakeBody()
        {
            Vector3 headPosition = _snakeLineRendererBody[0].GetPosition(0);
            Vector3 tailPosition = _snakeLineRendererBody[^1].GetPosition(1);

            head.position = headPosition;
            tail.position = tailPosition;

            RotateSnakeBody();
        }

        private void RotateSnakeBody()
        {
            Vector3 directionHead = (_snakeLineRendererBody[0].GetPosition(0) - _snakeLineRendererBody[0].GetPosition(1)).normalized;
            head.rotation = Quaternion.FromToRotation(head.right, directionHead) * head.rotation;

            Vector3 directionTail = (_snakeLineRendererBody[^1].GetPosition(0) - _snakeLineRendererBody[^1].GetPosition(1)).normalized;
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

        public void InstantiateBody(List<Vector3> bodyPositions)
        {
            _snakeLineRendererBody.Add(snakeBodyLine);

            for (int i = 0; i < bodyPositions.Count - 1; i++)
            {
                if (i != 0)
                {
                    _snakeLineRendererBody.Add(Instantiate(snakeBodyLine, transform));
                }
                _snakeLineRendererBody[i].positionCount = 2;
                _snakeLineRendererBody[i].SetPosition(0, bodyPositions[i]);
                _snakeLineRendererBody[i].SetPosition(1, bodyPositions[i + 1]);
            }

            UpdateSnakeBody();
        }

        public void GrowSnakeBody()
        {
            LineRenderer newLineRenderer = Instantiate(_snakeLineRendererBody[_snakeLineRendererBody.Count - 1], transform);
            _snakeLineRendererBody.Add(newLineRenderer);
            newLineRenderer.positionCount = 2;
        }

        public void UpdateBodyLine(int index, Vector3 point, Vector3 nextPoint)
        {
            _snakeLineRendererBody[index].SetPosition(0, point);
            _snakeLineRendererBody[index].SetPosition(1, nextPoint);
        }

        public void ResetBody()
        {
            for (int i = 1; i < _snakeLineRendererBody.Count; i++)
            {
                Destroy(_snakeLineRendererBody[i].gameObject);
            }
            
            _snakeLineRendererBody.Clear();
        }
    }
}