
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon", menuName = "Create Weapon")] 
public class Weapon : ScriptableObject
{
    [SerializeField] private string weaponName;
    [SerializeField] private Sprite icon;
    [SerializeField] private int damage;
    [SerializeField] private Bullet bulletReference;
    [SerializeField] private string fireMode;
    [SerializeField] private float fireRate;
    [SerializeField] private int bulletsPerClick;
    [SerializeField] private int magazineCapacity;
    [SerializeField] private float reloadTime;
    private int bulletCounter;
    private Time lastShot;

   public void Fire(Vector2 position, Quaternion direction, string tag)
    {
        Bullet tempBullet = Object.Instantiate(bulletReference, position, direction);
        tempBullet.SetUpBullet(tag, damage, direction);
        bulletCounter--;
        Debug.Log("New bullet created");
        
    }
    public void SemiAutomaticFire(Vector2 position, Quaternion direction, string tag)
    {
        int counter = 0;
        while (counter <= bulletsPerClick)
        {
            if (Time.time >= lastShot + fireRate)
            {
                Fire(position, direction, tag);
                counter++;
                FireRateTimer();
           }
            
        }
        

    }
    public void AutomaticFire(Vector2 position, Quaternion direction, string tag)
    {

    } 
   public void ShootMe(Vector2 position, Quaternion direction, string tag)
    {
        if (bulletCounter >= magazineCapacity)
        {
            Reload();
            return;
        }
        if (string.Equals(fireMode, "OnClick"))
        {
            Fire(position, direction, tag);
            Debug.Log("Pass to fire method");
        }
         if (fireMode == "Semi-Automatic")
        {
            SemiAutomaticFire(position, direction, tag);
        }   
        if (fireMode == "AutomaticFire")
        {
            while (Input.GetMouseButton(0))
            {
                Fire(position, direction, tag);
                FireRateTimer();
            }
        } 
        
    }
    IEnumerator FireRateTimer()
    {
        
        yield return new WaitForSecondsRealtime(fireRate);
        

    }
    IEnumerator Reload()
    {
        bulletCounter = magazineCapacity;
        yield return new WaitForSecondsRealtime(reloadTime);
    }
  
    public Weapon() 
    { 
        bulletCounter = magazineCapacity;
        Debug.Log("Weapon instance created, bulletCounter set to magazineCapacity");
    }
}

