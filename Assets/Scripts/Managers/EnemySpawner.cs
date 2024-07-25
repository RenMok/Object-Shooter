using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Singleton;

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float waveInterval;

    [SerializeField] private int maxActiveEnemies = 15;
    [SerializeField] private int enemiesInStartingWave = 5;
    [SerializeField] private int prevWave = 3;
  //  [SerializeField] private int numberOfWaves = 5;
    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(this);
        }
        else Singleton = this;
    }
    public void StartSpawning()
    {
        StartCoroutine(Spawner(enemiesInStartingWave));
    }
    private IEnumerator Spawner(int enemiesInWave)
    {
        int enemiesSpawned = 0;
        while (enemiesSpawned < enemiesInWave)
        {
            yield return new WaitUntil(() => 
                GameManager.Singleton.liveEnemies.Count < maxActiveEnemies);
            yield return new WaitForSeconds(spawnInterval);
            SpawnRandomEnemy();
            enemiesSpawned++;
        }
        yield return new WaitUntil(() => 
            GameManager.Singleton.liveEnemies.Count == 0);
        yield return new WaitForSecondsRealtime(waveInterval);
        NextWave(enemiesInWave);
    }
    private void SpawnRandomEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform randomSpawnPoint = spawnPoints[randomIndex];
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], 
            randomSpawnPoint.position, Quaternion.identity);
    }
    private void NextWave(int currWave)
    {
        int nextWave = currWave + prevWave;
        prevWave = currWave;
        StartCoroutine(Spawner(nextWave));
    }
    public void SpawnMinis(int numberToSpawn, int iterations, float spawnRadius,  Vector3 position, DividingEnemy miniVersionPrefab)
    {

        for (int i = 0; i < numberToSpawn; i++)
        {
            float x = Random.Range(-spawnRadius, spawnRadius);
            float y = Random.Range(-spawnRadius, spawnRadius);

            var temp = Instantiate(miniVersionPrefab, position +
                new Vector3(x, y), Quaternion.identity);
            temp.iterations = iterations;
            temp.maxHealth = iterations + 1;
        }
    }
}
