using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//Enemies and Players will derive from this class - all of them will have these characteristics
public class Player : Character
{
    [SerializeField] private Transform aim;
   // [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Weapon starterWeapon;
    private Weapon playerWeapon;
    public float speed;
    
    protected override void Start()
    {
        SetUpPlayer();
    }
    public void SetUpPlayer()
    {
        healthPoints = new Health(4);
        healthPoints.OnHealthChanged.AddListener(ChangedHealth);
        playerWeapon = Instantiate(starterWeapon);
        Debug.Log("Weapon instance set up in SetUpPlayer");
        transform.position = Vector3.zero;
    }
    public void ChangedHealth(int health)
    {
        if (health <= 0)
        {
            Die(); 
        }
        Debug.Log("Health has Changed");
    }
    public override void Attack()
    {
        playerWeapon.ShootMe(transform.position, transform.rotation, "Enemy");
        Debug.Log("Pass ShootMe method to Weapon");
    }
    public override void Move(Vector2 direction, float angle, float speed)
    {
        base.Move(direction, angle, speed);
    }

    public override void SetWeapon(Weapon weapon)
    {
        playerWeapon = weapon;
    }
    /* public float speed;
     public float rotationSpeed;
     public Rigidbody2D rb;
     float horizontal;
     float vertical;
     Vector2 direction;
     public Vector2 playerStart;
     Transform currentPosition;
     [SerializeField] Weapon weapon;
     Health healthPoints;
     bool isShooting;
     public Player(Player player)
     {
         weapon = player.AddComponent<Weapon>();
         //weapon = player.GetComponent<Weapon>();
         weapon.SetFireRate(0.2f);
         healthPoints = new Health(100);
        // rb = player.AddComponent<Rigidbody2D>();
        rb = player.GetComponent<Rigidbody2D>();
         rb.MovePosition(playerStart);

     }
     public void Move()
     {
         currentPosition = GetComponent<Transform>();   
         horizontal = Input.GetAxis("Horizontal");
         vertical = Input.GetAxis("Vertical");
         direction += new Vector2(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime);
         rb.MovePosition(direction);
         AlignRotation();
         isShooting = Input.GetMouseButton(0);
             if (isShooting == true)
         {
             do { weapon.Fire(); } while (Input.GetMouseButton(0));

         }
     }
     public void AlignRotation()
     {
        // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));

         Vector3 mousePosition = Input.mousePosition;
         Vector3 cam = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.y);
         Vector3 objectPosition = Camera.main.WorldToScreenPoint(rb.position);
         mousePosition.z = Vector3.Distance(currentPosition.position, cam);
         mousePosition.y -= objectPosition.y;
         mousePosition.x -= objectPosition.x;
         mousePosition.x *=   Time.deltaTime;
         mousePosition.y *=  Time.deltaTime;
         float angle = Mathf.Atan2(mousePosition.x, mousePosition.y) * Mathf.Rad2Deg;
         rb.MoveRotation(Quaternion.Slerp(currentPosition.rotation, Quaternion.Euler(0, 0, -angle), rotationSpeed)); 
     } */
}