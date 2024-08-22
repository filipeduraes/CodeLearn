using UnityEngine;

namespace CodeLearn.SnakeGame
{ 
    public class GameGrid : MonoBehaviour
    {
        [SerializeField] private float cellSize = 1f;
        [SerializeField] private int gridSize = 10;

        public int GridSize  => gridSize;

        public Vector3 ConvertIndexToWorldPosition(Vector2Int index)
        {
            float positionX = transform.position.x + index.x * cellSize + cellSize / 2;
            float positionY = transform.position.y + index.y * cellSize + cellSize / 2;

            return new Vector3(positionX,positionY, transform.position.z - 1);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    Vector2 center = ConvertIndexToWorldPosition(new Vector2Int(x, y));
                    Gizmos.DrawWireSphere(center,0.3f);
                }
            }
        }
    }
}
