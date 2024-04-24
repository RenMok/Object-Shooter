using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float time;
    [SerializeField] private float attackDistance;
    [SerializeField] private Bullet bulletPrefab;
    private Player target;
    public float speed;
    private SpriteRenderer spriteRenderer;
    
    private Weapon enemyWeapon;
    public float fireRate;
    protected override void Start()
    {
        SetUpEnemy(2);
    }
    public void SetUpEnemy(int healthParam)
    {
        target = FindObjectOfType<Player>();
        healthPoints = new Health(healthParam);
        healthPoints.OnHealthChanged.AddListener(ChangedHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyWeapon = ScriptableObject.CreateInstance<Weapon>();
        
    }

    public void ChangedHealth(int health)
    {
        Debug.Log("LIFE HAS CHANGED TO" + health);
        if (health <= 0)
        {
            Die();
        }
        
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = target.transform.position - this.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Move(direction, angle, speed);
        }
    }
    public Enemy (string enemyName)
    {

    }
    public Enemy()
    {
        
    }

    public override void Attack()
    {
        target.ReceiveDamage();
    }
   
    public void Shoot()
    {
        enemyWeapon.ShootMe(transform.position, transform.rotation, "Player");
    }
    public override void Move(Vector2 direction, float angle, float speed)
    {
        // if distance from target is lesser than attackDistance
        if (Vector2.Distance(target.transform.position, transform.position) > attackDistance)
        {
            base.Move(direction, angle, speed);
          /*  if (direction.x <= 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            { spriteRenderer.flipX = false; } */
            if (direction.x <= 0)
            { spriteRenderer.flipY = true; }
            else { spriteRenderer.flipY = false; }
        }
        else // every time the enemy is close to the player
        {
            //stop immediately
            rigidBody.velocity = Vector2.zero;
            time += Time.deltaTime;
            if (time >= 3f)
            {
                Attack();
                time = 0;
            }
        }
    }

}
