using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefabs;

    [SerializeField]
    private GameObject playerPrefab;

    public float spawnInterval = 2f;
    public float spawnRectangleWidth = 20f;
    public float spawnRectangleHeight = 12f;
    //public float moveSpeed = 5f;

    void Start()
    {
        InvokeRepeating("SpawnEnemies", 0f, spawnInterval);
    }

    void SpawnEnemies()
    {
        // Pozycja gracza
        Vector3 playerPosition = playerPrefab.transform.position;

        // Losowe miejsce na obwodzie prostokąta wokół gracza
        float randomX;
        float randomY;

        float randomEdge = Random.Range(0f, 1f);

        if (randomEdge < 0.25f) // lewa krawędź
        {
            randomX = playerPosition.x - spawnRectangleWidth / 2f;
            randomY = Random.Range(playerPosition.y - spawnRectangleHeight / 2f, playerPosition.y + spawnRectangleHeight / 2f);
        }
        else if (randomEdge < 0.5f) // górna krawędź
        {
            randomX = Random.Range(playerPosition.x - spawnRectangleWidth / 2f, playerPosition.x + spawnRectangleWidth / 2f);
            randomY = playerPosition.y + spawnRectangleHeight / 2f;
        }
        else if (randomEdge < 0.75f) // prawa krawędź
        {
            randomX = playerPosition.x + spawnRectangleWidth / 2f;
            randomY = Random.Range(playerPosition.y - spawnRectangleHeight / 2f, playerPosition.y + spawnRectangleHeight / 2f);
        }
        else // dolna krawędź
        {
            randomX = Random.Range(playerPosition.x - spawnRectangleWidth / 2f, playerPosition.x + spawnRectangleWidth / 2f);
            randomY = playerPosition.y - spawnRectangleHeight / 2f;
        }

        Vector3 randomPosition = new Vector3(randomX, randomY, 0f);



        // Losowy wybór prefabu wroga
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Tworzenie wroga na losowej pozycji
        GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

        // Obróć wroga w kierunku gracza 
        Vector2 directionToPlayer = (playerPrefab.transform.position - enemy.transform.position).normalized;
        enemy.transform.up = directionToPlayer;
        Debug.Log("Pozycja wroga: " + randomPosition);

         //Dodaj tag "Enemy" i komponent Collider do wroga
        enemy.tag = "Enemy";
        enemy.AddComponent<BoxCollider>(); // lub inny odpowiedni Collider w zależności od potrzeb


    }
}
