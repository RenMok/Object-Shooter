using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public float speed;
    public Weapon weapon;
    public Health health;
    


    public void Move()
    {

    }
    public void Shoot()
    {

    }

    public Enemy()
    {
  
        health = new Health(Random.Range(10, 75));
        weapon = new Weapon(Random.Range(0.1f, 2f));
    }

}
