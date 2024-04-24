using UnityEngine.Events;
using UnityEngine;

public class Health
{
    private int currentHealth;
    public UnityEvent<int> OnHealthChanged;
    
    
    public void DecreaseLife(int damage)
    {
        currentHealth -= damage;
        OnHealthChanged.Invoke(currentHealth);
    }
    // Allows the method to pass without a parameter, defaulting to damage of 1
    public void DecreaseLife()
    {
        currentHealth -= 1;
        OnHealthChanged.Invoke(currentHealth);
    }
    public void DecreaseLife(int damage, float timer)
    {
        // for damage over time?
    }
    public void IncreaseLife (int heal)
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
        OnHealthChanged = new UnityEvent<int>();
    }
}
