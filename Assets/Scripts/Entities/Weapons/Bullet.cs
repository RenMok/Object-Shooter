using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float lifeSpan;
    private string targetTag;
    private int damage;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }
    public void SetUpBullet(string target, int damage)
    {
        targetTag = target;
        this.damage = damage;
    }
    private void Update()
    {
        transform.Translate(bulletSpeed * Time.deltaTime * Vector2.right);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            ExplosionManager.Singleton.OnHit(transform);
            collision.GetComponent<IDamageable>().ReceiveDamage(damage);
            Destroy(gameObject);
        }
    }
}


