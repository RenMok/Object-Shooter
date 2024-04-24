using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Character : MonoBehaviour, IDamageable
{
    private int strength;
    protected Health healthPoints;

    [SerializeField] protected Rigidbody2D rigidBody;
    protected virtual void Start()
    {
        // Create a default health value if no parameter is passed on character creation
        healthPoints = new Health(5);
    }
    public abstract void Attack();
    public virtual void Die()
    {
        Destroy(gameObject);
    }
    public virtual void Move(Vector2 direction, float angle, float speed)
    {
        rigidBody.AddForce(direction.normalized * speed * Time.deltaTime * 500f, ForceMode2D.Impulse);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    // Default constructor with no parameters, creates a character with speed of 5 and health of 4
    public void ReceiveDamage(int damage)
    {
        healthPoints.DecreaseLife(damage);
    }
    public void ReceiveDamage()
    {
        healthPoints.DecreaseLife();
    }
    public virtual void SetWeapon(Weapon newWeapon)
    {

    }
}
