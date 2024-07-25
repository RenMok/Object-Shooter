using UnityEngine;

public class WeaponPowerUp : PickUp
{
    [SerializeField] private WeaponData newWeapon;
    [SerializeField] private float duration;

    private void Start()
    {
        newWeapon.lifeSpan = duration;
    }
    protected override void PickMe(Player user)
    {
        user.weapon.ReceiveWeaponData(newWeapon);
        BuffUI.Singleton.ActivateBuffUI(duration, user);
        Destroy(gameObject);
    }
}
