using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class ExplosionManager : MonoBehaviour
{
    public static ExplosionManager Singleton;

    [SerializeField] private float explosionRadius;
    [SerializeField] private float secondaryExplosionDelay;
    [SerializeField] private float playerDeathRadius;
    [SerializeField] private float playerDeathDelay;
    [SerializeField] private int playerDeathExplosions;
    [SerializeField] private GameObject hitAnimationPrefab;
    [SerializeField] private Queue<GameObject> hitAnimationPool = new();
    [SerializeField] private GameObject explodeAnimationPrefab;
    [SerializeField] private Queue<GameObject> explodeAnimationPool = new();

    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(this);
        }
        else Singleton = this;
    }
    private void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            var temp = Instantiate(hitAnimationPrefab, transform);
            temp.SetActive(false);
            hitAnimationPool.Enqueue(temp);
            temp = Instantiate(explodeAnimationPrefab, transform);
            temp.SetActive(false);
            explodeAnimationPool.Enqueue(temp);
        }
    }
    public void OnHit(Transform bullet)
    {
        if (hitAnimationPool.Peek() != null)
        {
            GameObject temp = hitAnimationPool.Dequeue();
            temp.SetActive(true);
            temp.transform.position = bullet.position;

        }
        else
        {
            Instantiate(hitAnimationPrefab, bullet.position, bullet.rotation, transform);
        }
    }
    public void ReturnToHitPool(GameObject animationObject)
    {
        animationObject.SetActive(false);
        hitAnimationPool.Enqueue(animationObject);
    }

    public void ReturnToExplosionPool(GameObject animationObject)
    {
        animationObject.SetActive(false);
        explodeAnimationPool.Enqueue(animationObject);
    }
    public void ExplodePlayer(Transform location)
    {
        ExplodeFromPool(location.position);
        StartCoroutine(SecondaryExplosions(location.position, playerDeathExplosions, playerDeathRadius,
            playerDeathDelay));
    }
    public void Explode(Transform location)
    {
        ExplodeFromPool(location.position);
        StartCoroutine(SecondaryExplosions(location.position, 2, explosionRadius, secondaryExplosionDelay));
    }
    IEnumerator SecondaryExplosions(Vector2 position, int explosions, float radius, float delay)
    {
        for (int i = 0; i < explosions; i++)
        {
            yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(delay - 0.1f, delay + 0.1f));
            ExplodeFromPool(RandomizeLocation(position, radius));
        }
    }

    private void ExplodeFromPool(Vector2 position)
    {
        if (explodeAnimationPool.Peek() == null)
        {
            Instantiate(explodeAnimationPrefab, position, Quaternion.identity, transform);
        }
        else
        {
            try
            {
                GameObject temp = explodeAnimationPool.Dequeue();
                temp.SetActive(true);
                temp.transform.position = position;
            }
            finally
            {
                Instantiate(explodeAnimationPrefab, position, Quaternion.identity, transform);
            }
        }
        
    }

    public Vector2 RandomizeLocation(Vector2 position, float radius)
    {
        float x = UnityEngine.Random.Range(-radius, radius) + position.x;
        float y = UnityEngine.Random.Range(-radius, radius) + position.y;
        return new Vector2(x, y);
    }
}
