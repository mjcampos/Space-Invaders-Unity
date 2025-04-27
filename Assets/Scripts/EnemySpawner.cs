using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] GameObject[] enemyPrefabs = new GameObject[4];
    [SerializeField] int enemiesPerRow = 5;
    [SerializeField] float horizontalSpacing = 2f;
    [SerializeField] float verticalSpacing = 2f;

    int _rows = 4;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        SpawnEnemies();
        RecenterParent();
    }

    void SpawnEnemies() {
        for (int row = 0; row < _rows; row++) {
            // Calculate the total width of the enemies in this row, including horizontal spacing
            float rowWidth = 0f;
            
            for (int col = 0; col < enemiesPerRow; col++) {
                GameObject enemyPrefab = enemyPrefabs[row % enemyPrefabs.Length];
                SpriteRenderer enemyRenderer = enemyPrefab.GetComponent<SpriteRenderer>();
                
                rowWidth += enemyRenderer.bounds.size.x + horizontalSpacing;  // Add width and spacing of each enemy
            }

            // Remove the extra spacing added at the end of the row
            rowWidth -= horizontalSpacing;

            // Calculate the starting position for this row to center it
            float startX = -(rowWidth / 2f);  // Center the row horizontally

            for (int col = 0; col < enemiesPerRow; col++) {
                // Choose the prefab for this enemy
                GameObject enemyPrefab = enemyPrefabs[row % enemyPrefabs.Length];

                // Get the width of the current enemy prefab
                SpriteRenderer enemyRenderer = enemyPrefab.GetComponent<SpriteRenderer>();

                // Calculate the spawn position for this enemy
                Vector3 spawnPosition = new Vector3(
                    transform.position.x + startX + col * (enemyRenderer.bounds.size.x + horizontalSpacing),  // Adjust for spacing
                    transform.position.y - row * verticalSpacing,    // Keep vertical spacing
                    transform.position.z
                );

                // Instantiate the enemy at the calculated position and set the parent correctly
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                // Parent the enemy to the EnemySpawner (this object)
                enemy.transform.SetParent(transform);
            }
        }
    }

    void RecenterParent() {
        // Get the renderer bounds of all child enemies
        Renderer[] allRenderers = GetComponentsInChildren<Renderer>();
        
        if (allRenderers.Length > 0) {
            Bounds combinedBounds = allRenderers[0].bounds;
            
            foreach (Renderer r in allRenderers) {
                combinedBounds.Encapsulate(r.bounds); // Get the bounding box of all enemies
            }

            // Center the parent object based on the combined bounds
            transform.position = new Vector3(-combinedBounds.center.x, transform.position.y, transform.position.z);
        }
    }
}
