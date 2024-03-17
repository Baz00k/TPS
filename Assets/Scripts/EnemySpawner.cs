using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnRectangleWidth = 20f;
    [SerializeField] private float spawnRectangleHeight = 12f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemies), 0f, spawnInterval);
    }

    void SpawnEnemies()
    {
        float randomX;
        float randomY;

        // Choose a random position on the spawn rectangle
        if (Random.Range(0, 2) == 0)
        {
            randomX = Random.Range(-spawnRectangleWidth / 2, spawnRectangleWidth / 2);
            randomY = Random.Range(-spawnRectangleHeight / 2, spawnRectangleHeight / 2);
        }
        else
        {
            if (Random.Range(0, 2) == 0)
            {
                randomX = Random.Range(-spawnRectangleWidth / 2, spawnRectangleWidth / 2);
                randomY = spawnRectangleHeight / 2;
            }
            else
            {
                randomX = spawnRectangleWidth / 2;
                randomY = Random.Range(-spawnRectangleHeight / 2, spawnRectangleHeight / 2);
            }
        }

        Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

        // Choose a random enemy prefab
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Spawn the enemy
        GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }
}
