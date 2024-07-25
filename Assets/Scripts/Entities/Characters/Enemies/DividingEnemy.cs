using UnityEngine;

public class DividingEnemy : Enemy
{
    [SerializeField] internal int numberToSpawn, iterations;
    [SerializeField] private DividingEnemy miniVersionPrefab;
    [SerializeField] private float spawnRadius;
    public override void Die()
    {
        if (iterations > 0)
        {
            iterations--;
            EnemySpawner.Singleton.SpawnMinis(numberToSpawn, iterations, spawnRadius, transform.position, miniVersionPrefab);
        }
            base.Die();
    }
}

