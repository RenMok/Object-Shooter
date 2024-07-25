using UnityEngine;

public class PooledItems : MonoBehaviour
{
    public void ReturnHit()
    {
        ExplosionManager.Singleton.ReturnToHitPool(gameObject);
    }
    public void ReturnExplosion()
    {
        ExplosionManager.Singleton.ReturnToExplosionPool(gameObject);
    }
}
