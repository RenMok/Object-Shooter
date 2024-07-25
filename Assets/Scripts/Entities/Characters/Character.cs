
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    public int maxHealth;
    [SerializeField] internal Health healthPoints;
    [SerializeField] internal Weapon weapon;
    [SerializeField] protected Rigidbody2D rigidBody;

    public AudioClip onHitSound;
    public AudioClip onDeathSound;

    protected Character target;
    public virtual void Attack()
    {
        weapon.ShootMe(target.tag);
    }
    public virtual void Die()
    {
        AudioSource.PlayClipAtPoint(onDeathSound, Camera.main.transform.position);
        ExplosionManager.Singleton.Explode(transform);
        Destroy(gameObject);
    }
    public virtual void Move(Vector2 direction, float angle, float speed)
    {
        rigidBody.AddForce(500f * speed * Time.deltaTime * direction.normalized, ForceMode2D.Impulse);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public void ReceiveDamage(int damage)
    {
        AudioSource.PlayClipAtPoint(onHitSound, Camera.main.transform.position);
        healthPoints.DecreaseLife(damage);
    }
    public void ReceiveDamage()
    {
        ReceiveDamage(1);
    }
    public virtual void SetWeapon(Weapon newWeapon)
    {
        newWeapon.owner = this;
        weapon = newWeapon;
    }
}
