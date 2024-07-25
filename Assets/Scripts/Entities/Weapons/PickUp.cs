using UnityEngine;

public abstract class PickUp : MonoBehaviour , INukeable
{
    [SerializeField] private Renderer _renderer;
    private void Awake()
    {
        if (_renderer == null)
        {
            _renderer = GetComponent<Renderer>();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // Only allows the player to pick up
        if (collision.gameObject.CompareTag("Player"))
        {
            Player recipient = collision.gameObject.GetComponent<Player>();
            PickMe(recipient);
        }
    }
    protected abstract void PickMe(Player user);

    // Pickups can be destroyed by nuke
    public void OnNuke()
    {
        // Play explosion animation
        ExplosionManager.Singleton.Explode(transform);
        Destroy(gameObject);
    }
}
