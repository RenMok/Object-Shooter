using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton;

    [SerializeField] private Transform origin;
    [SerializeField] private KillCounter score;
    [SerializeField] private PickUpSpawner pickupSpawner;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private float gameOverTransitionTime; // Waits for explosion animations to finish

    public List<Enemy> liveEnemies = new(); // Lists all currently active enemies

    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(this);
        }
        else Singleton = this;
        StartCoroutine(StartGame());
    }
    public void RemoveFromLiveEnemies(Enemy enemy)
    {
        liveEnemies.Remove(enemy);
        score.IncreaseScore();
        pickupSpawner.SpawnPickUpAt(enemy.transform.position);
    }
    public void AddToLiveEnemies(Enemy enemy)
    {
        liveEnemies.Add(enemy);
    }
    public void GameOver()
    {
        StartCoroutine(SwitchToGameOver());
        EnemySpawner.Singleton.StopAllCoroutines();
    }
    private IEnumerator SwitchToGameOver()
    {
        yield return new WaitForSecondsRealtime(gameOverTransitionTime);
        SceneManager.LoadScene(2);

    }
    // Functions as a late start to ensure GameManager can find references to important objects
    public IEnumerator StartGame()
    {
        yield return new WaitForSecondsRealtime(1);
        if (score == null)
        {
            score = FindAnyObjectByType<KillCounter>();
        }
        // Create new player with listener that calls GameOver upon death
        Instantiate(playerPrefab, origin).GetComponent<Player>().PlayerDied.AddListener(GameOver);
        EnemySpawner.Singleton.StartSpawning();
    }
    /*
    public void RestartGame()
    {
        StartCoroutine(StartGame());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }
    */

}
