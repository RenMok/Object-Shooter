using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public int currentHealth;
    
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
    public void Heal (int heal)
    {
        currentHealth += heal;  
    }
    public int GetHealth()
    {
        return currentHealth;
    }
    public Health(int maxHealth)
    {
        currentHealth = maxHealth;
    }
}
