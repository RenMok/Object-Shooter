using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    private string targetTag;
    private int damage;

    private void Start()
    {
       
        Destroy(gameObject, 5f);
    }
    public void SetUpBullet(string tag, int damage, Quaternion direction)
    {
        targetTag = tag;
        this.damage = damage;
        Debug.Log("Setup bullet");
    }
    private void Update()
    {
        transform.Translate(bulletSpeed * Time.deltaTime * Vector2.right);
        Debug.Log("Bullet moving");
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(targetTag))
        {
            // Do damage to damageable entity
            collision.GetComponent<IDamageable>().ReceiveDamage(damage);
            Destroy(gameObject);
            
        }
    }
}


