using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public Rigidbody2D rb;
    float horizontal;
    float vertical;
    Vector2 direction;
    public Vector2 playerStart;
    Transform currentPosition;
    Weapon weapon;
    Health health;
    public void Move()
    {
        currentPosition = transform;   
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        direction += new Vector2(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime);
        rb.MovePosition(direction);
        AlignRotation();
    }
    public void Shoot()
    {

    }
    
    public void AlignRotation()
    {
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
    }
    
    public Player(Player player)
    {
        weapon = new Weapon(2);
        health = new Health(100);
        rb.MovePosition(playerStart);
    }   
}