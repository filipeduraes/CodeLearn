using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeLearn.SnakeGame
{
    public class SnakeBody : MonoBehaviour
    {
        [SerializeField] public LineRenderer SnakeBodyLine;
        [SerializeField] private Transform head;
        [SerializeField] private Transform tail;

       
        private void Update()
        {
            UpdateSnakeBody();
        }

        public void UpdateSnakeBody()
        {
            Vector3 headPosition = SnakeBodyLine.GetPosition(0);
            Vector3 tailPosition = SnakeBodyLine.GetPosition(SnakeBodyLine.positionCount - 1);

            head.position = headPosition;
            tail.position = tailPosition;


            Vector3 directionHead = (head.position - SnakeBodyLine.GetPosition(1)).normalized;
            head.rotation = Quaternion.FromToRotation(head.right, directionHead) * head.rotation;

            Vector3 directionTail = (SnakeBodyLine.GetPosition(SnakeBodyLine.positionCount - 1) - tail.position).normalized;
            tail.rotation = Quaternion.FromToRotation(tail.right, directionTail) * tail.rotation;
        }

        public void UpdateSnakeGrow()
        {
            SnakeBodyLine.positionCount++;
        }

        private void OnEnable()
        {
            SnakeGame.OnSnakeGrow += UpdateSnakeGrow;
        }

        private void OnDisable()
        {
            SnakeGame.OnSnakeGrow -= UpdateSnakeGrow;
        }

    }
}