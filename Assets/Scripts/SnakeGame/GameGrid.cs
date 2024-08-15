using UnityEngine;

namespace CodeLearn.SnakeGame
{ 
    public class GameGrid : MonoBehaviour
    {
        [SerializeField] private float cellSize = 1f;

        public Vector2 ConvertIndexToWorldPosition(int x, int y)
        {
            float positionX = transform.position.x + x * cellSize + cellSize / 2;
            float positionY = transform.position.y + y * cellSize + cellSize / 2;

            return new Vector2(positionX,positionY);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Vector2 center = ConvertIndexToWorldPosition(x, y);
                    Gizmos.DrawWireSphere(center,0.3f);
                }
            }
        }
    }
}
