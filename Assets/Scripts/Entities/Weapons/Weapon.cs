using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


// The weapon class is a universal weapon, which receives weapon data to define its behaviour
public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData currentWeapon;
    public WeaponData defaultWeapon;
    internal Character owner;
    private Coroutine powerUp;
    private string target;
    private bool isFiring = false;

    private void Start()
    {
        powerUp = null;
        if (currentWeapon == null)
        {
            currentWeapon = ScriptableObject.CreateInstance<WeaponData>();
            currentWeapon = defaultWeapon;
        }

    }
    // Instantiates 1 bullet
    private void Fire()
    {
        if (currentWeapon.onFireSound != null)
        {
            AudioSource.PlayClipAtPoint(currentWeapon.onFireSound, Camera.main.transform.position);
        }

        Bullet tempBullet = Instantiate(currentWeapon.bulletReference, transform.position, transform.rotation);
        tempBullet.SetUpBullet(target, currentWeapon.damage);
    }
    // Command received from a Character class
    public void ShootMe(string target)
    {

        this.target = target;

        if (!isFiring)
        {
            isFiring = true;
            StartCoroutine(AutomaticFire());
        }
    }

    private IEnumerator AutomaticFire()
    {
        int i = 0; // Counter for shots per click
        while (isFiring)
        {
            // Fires 1 bullet instantly
            Fire();
            yield return new WaitForSeconds(currentWeapon.shotDelay);
            // Behaves differently depending on weapon type
            switch (currentWeapon.Type)
            {
                // 1 click = 1 shot
                case WeaponData.WeaponType.Standard:
                    goto default;
                // 1 click = multiple shots (shotPerClick)
                case WeaponData.WeaponType.SemiAutomatic:
                    i++;
                    if (i < currentWeapon.shotPerClick) continue;
                    else goto default;
                // Continues shooting so long as mouse button is held down
                case WeaponData.WeaponType.Automatic:
                    if (Input.GetMouseButton(0)) continue;
                    else goto default;

                    // Reset and exit coroutine
                    default:
                    isFiring = false;
                    break;
            }
        }
        
    }

    // Called to replace the weapon at runtime, usually from a pickup
    internal void ReceiveWeaponData(WeaponData weaponData)
    {
        // Permanent weapons swap out weapon variables
        if (weaponData.Status == WeaponData.WeaponStatus.Permanent)
        {
            currentWeapon = weaponData;
            // Permanent weapon becomes the default weapon
            defaultWeapon = weaponData;
        }
        // Temporary weapons trigger a power-up, which resets to default after some time
        if (weaponData.Status == WeaponData.WeaponStatus.Temporary)
        {
            // Restarts the power up if it is already running
            if (powerUp != null)
            {
                StopCoroutine(powerUp);
            }
            powerUp = StartCoroutine(PowerUp(weaponData));
        }
    }

    private IEnumerator PowerUp(WeaponData newWeapon)
    {
        currentWeapon = newWeapon;
        yield return new WaitForSeconds(newWeapon.lifeSpan);
        currentWeapon = defaultWeapon;
        powerUp = null;
    }
}
