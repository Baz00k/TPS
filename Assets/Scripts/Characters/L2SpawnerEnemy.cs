using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class L2SpawnerEnemy : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Prefab of the enemy.")]
    private GameObject enemyPrefab;

    [SerializeField]
    [Tooltip("Interval between enemy spawns.")]
    private float spawnInterval = 20f;
    protected void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }
    // Method to get a random point on the NavMesh
    private Vector3 GetRandomNavMeshPoint()
    {
        Vector3 randomPoint = Vector3.zero;
        NavMeshHit hit;
        // Losowy punkt wewnątrz prostokąta określonego przez minPoint i maxPoint
        Vector3 minPoint = new Vector3(-150f, -150f, 0f); // Określ minimalny punkt w obszarze NavMesh
        Vector3 maxPoint = new Vector3(150f, 150f, 0f); // Określ maksymalny punkt w obszarze NavMesh

        for (int i = 0; i < 100; i++) // Spróbuj 30 razy, aby znaleźć punkt na NavMesh
        {
            // Losowy punkt wewnątrz obszaru minPoint i maxPoint
            Vector3 randomPosition = new Vector3(Random.Range(minPoint.x, maxPoint.x), Random.Range(minPoint.y, maxPoint.y), 0f);


            if (NavMesh.SamplePosition(randomPosition, out hit, 1.0f, NavMesh.AllAreas)) // Sprawdź, czy punkt jest na NavMesh
            {
                if (hit.hit) // Sprawdź, czy punkt jest na obszarze "walkable"
                {
                    randomPoint = hit.position;
                    break; // Jeśli punkt jest na obszarze "walkable", przerwij pętlę
                }
            }
        }

        return randomPoint;
    }
    // Coroutine to spawn enemies at regular intervals
    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    // Method to spawn an enemy at a random point on the NavMesh
    private void SpawnEnemy()
    {
        Vector3 spawnPoint = GetRandomNavMeshPoint();
        Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
    }
}
