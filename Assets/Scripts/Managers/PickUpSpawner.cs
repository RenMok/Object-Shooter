using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private float spawnChance = 25f;
    [SerializeField] private List<GameObject> pickUpPrefabs = new List<GameObject>();
    [SerializeField] private float despawnTime = 10f;

    // Called from GameManager when an enemy is removed
    internal void SpawnPickUpAt(Vector2 position)
    {
        if (!RandomChance())
        {
            return;
        }
        else
        {   // Instantiate a pickup which will be destroyed after a few seconds
            StartCoroutine(Despawn(Instantiate(RandomPickUp(), position, Quaternion.identity)));
        }
    }
    // Returns true if a pick-up should be spawned
    private bool RandomChance()
    {
        if (Random.Range(0f, 100f) < spawnChance)
        {
            return true;
        }
        else return false;
    }
    // Returns a random pickup from the list of prefabs
    private GameObject RandomPickUp()
    {
        return pickUpPrefabs[Random.Range(0, pickUpPrefabs.Count)];
    }

    // Removes pick-up if not retrieved within time-limit
    private IEnumerator Despawn(GameObject target)
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(target);
    }
}
