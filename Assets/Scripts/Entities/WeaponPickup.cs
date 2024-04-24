using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : PickUp
{
    [SerializeField] private Weapon newWeapon;
    // Start is called before the first frame update
    protected override void PickMe(Character character)
    {
        
        base.PickMe(character);
        character.SetWeapon(newWeapon);
    }
}
