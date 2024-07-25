using UnityEngine;

public class Enemy : Character, INukeable
{ 
    [SerializeField] protected float attackDistance;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float speed;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    protected float time = 0;

    protected void Start()
    {
        SetUpEnemy();
    }
    public void SetUpEnemy()
    {
        healthPoints = new Health(maxHealth);
        healthPoints.OnHealthChanged.AddListener(ChangedHealth);
        GameManager.Singleton.AddToLiveEnemies(this);
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (target == null)
        {
            target = FindObjectOfType<Player>();
        }
    }

    public void ChangedHealth(int health)
    {
        if (health <= 0)
        {
            Die();
        }

    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            Move();
        }
        else target = FindAnyObjectByType<Player>();
    }
    public Enemy()
    {

    }
    public virtual void Move()
    {
        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // Move toward player if outside of attack distance
        if (Vector2.Distance(target.transform.position, transform.position) > attackDistance)
        {
            base.Move(direction, angle, speed);
        }
        else
        {
            // Stop immediately
            rigidBody.velocity = Vector2.zero;
            // No movement, rotation only
            base.Move(Vector2.zero, angle, 0);
            if (time <= 0)
            {
                Attack();
                time = fireRate;
            }
        }
        // flip sprite to face the right direction
        if (direction.x <= 0)
        {
            spriteRenderer.flipY = true;
        }
        else
        {
            spriteRenderer.flipY = false;
        }
        time -= Time.deltaTime;
    }
    public void OnNuke()
    {
        Die();
    }
    public override void Die()
    {
        GameManager.Singleton.RemoveFromLiveEnemies(this);
        base.Die(); 

    }
}
