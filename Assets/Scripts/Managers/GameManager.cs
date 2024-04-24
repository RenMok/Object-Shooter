using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public ScoreManager scoreManager;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Transform origin;
    // Start is called before the first frame update
    private void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        playerPrefab = GetComponent<Player>();  
       // Player player = Instantiate(playerPrefab,origin.position , Quaternion.identity);
       // player.SetUpPlayer();
        SpawnEnemy();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SpawnEnemy();
        }
        
    }
    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform randomSpawnPoint  = spawnPoints[randomIndex];
        Enemy enemy = Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.identity);
        enemy.SetUpEnemy(1);

    }
     public static IEnumerator Timer(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
    }
}
