using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy reference")]
    [SerializeField] GameObject[] enemies;
    [SerializeField] int minEnemies = 1;
    [SerializeField] int maxEnemies = 20;
    [SerializeField] float spawnHeight = 5f;
    [SerializeField] Vector2 spawnRangeX = new Vector2(-70, 70);
    [SerializeField] Vector2 spawnRangeZ = new Vector2(-50, 50);

    void Start()
    {
        int numberOfEnemies = Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < numberOfEnemies; i++)
        {
            int randomIndex = Random.Range(0, enemies.Length);

            Vector3 randomSpawnPosition = new Vector3(Random.Range(spawnRangeX.x, spawnRangeX.y), spawnHeight, 
                Random.Range(spawnRangeZ.x, spawnRangeZ.y));

            Instantiate(enemies[randomIndex], randomSpawnPosition, Quaternion.identity);
        }
    }
}
