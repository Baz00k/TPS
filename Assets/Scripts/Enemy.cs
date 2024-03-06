using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private GameObject playerPrefab;

    public float spawnInterval = 2f;
    public float spawnRectangleWidth = 20f;
    public float spawnRectangleHeight = 12f;

    void Start()
    {
        InvokeRepeating("SpawnEnemies", 0f, spawnInterval);
    }

    void SpawnEnemies()
    {
        float randomX;
        float randomY;

        float randomEdge = Random.Range(0f, 1f);

        if (randomEdge < 0.25f) // lewa krawędź
        {
            randomX = playerPrefab.transform.position.x - spawnRectangleWidth / 2f;
            randomY = Random.Range(playerPrefab.transform.position.y - spawnRectangleHeight / 2f, playerPrefab.transform.position.y + spawnRectangleHeight / 2f);
        }
        else if (randomEdge < 0.5f) // górna krawędź
        {
            randomX = Random.Range(playerPrefab.transform.position.x - spawnRectangleWidth / 2f, playerPrefab.transform.position.x + spawnRectangleWidth / 2f);
            randomY = playerPrefab.transform.position.y + spawnRectangleHeight / 2f;
        }
        else if (randomEdge < 0.75f) // prawa krawędź
        {
            randomX = playerPrefab.transform.position.x + spawnRectangleWidth / 2f;
            randomY = Random.Range(playerPrefab.transform.position.y - spawnRectangleHeight / 2f, playerPrefab.transform.position.y + spawnRectangleHeight / 2f);
        }
        else // dolna krawędź
        {
            randomX = Random.Range(playerPrefab.transform.position.x - spawnRectangleWidth / 2f, playerPrefab.transform.position.x + spawnRectangleWidth / 2f);
            randomY = playerPrefab.transform.position.y - spawnRectangleHeight / 2f;
        }

        Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

        // Losowy wybór prefabu wroga
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Tworzenie wroga na losowej pozycji
        GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

        // Obróć wroga w kierunku gracza 
        Vector2 directionToPlayer = (playerPrefab.transform.position - enemy.transform.position).normalized;
        enemy.transform.up = directionToPlayer;

        // Dodaj tag "Enemy" i komponent Collider do wroga
        enemy.tag = "Enemy";
        enemy.AddComponent<BoxCollider>(); // lub inny odpowiedni Collider w zależności od potrzeb
    }
}
