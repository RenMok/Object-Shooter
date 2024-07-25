using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
//Enemies and Players will derive from this class - all of them will have these characteristics
public class Player : Character
{
    [SerializeField] private Transform buffPlacement;
    public UnityEvent PlayerDied;
    public float speed;

    protected void Awake()
    {
        Setup();
    }
    private void Setup()
    {
        weapon = GetComponentInChildren<Weapon>();
        healthPoints = new Health(maxHealth);
        healthPoints.OnHealthChanged.AddListener(ChangedHealth);
        transform.position = Vector3.zero;
    }
    public void ChangedHealth(int health)
    {
        if (health <= 0)
        {
            PlayerDied.Invoke();
            ExplosionManager.Singleton.ExplodePlayer(transform);
            Die();
        }
    }
    public void UseNuke()
    {
        if (NukeUI.Singleton.availableNukes > 0)
        {
            // Finds all objects with a visual component
            SpriteRenderer[] list = FindObjectsOfType<SpriteRenderer>();
            foreach (SpriteRenderer renderer in list)
            {
                // Finds all visible objects
                if (renderer.isVisible)
                {
                    // Nukes any nukeable objects
                    if (renderer.TryGetComponent(out INukeable nukeable))
                    {
                        nukeable.OnNuke();
                    }
                    else continue;
                }
            }
            // Nuke expended
            NukeUI.Singleton.RemoveNuke();
        }
        else return;
        
    }
    public override void Attack()
    {
        // Bullets will collide with and damage any object tagged as an enemy
        weapon.ShootMe("Enemy");
    }

    // Called by the UI while a buff is active to maintain a position beside the player
    public Vector3 GetBuffPlacement()
    {
        return buffPlacement.position;
    }
}