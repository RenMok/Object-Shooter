using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Create Weapon")]
public class WeaponData : ScriptableObject
{
    public string WeaponName;
    public WeaponType Type;
    public WeaponStatus Status;
    public AudioClip onFireSound;
    [SerializeField] internal float shotDelay;
    [SerializeField] internal Bullet bulletReference;
    [SerializeField] internal int damage;
    [SerializeField] internal float lifeSpan;
    [SerializeField] internal int shotPerClick;
    

    public enum WeaponType
    {
        Standard,
        SemiAutomatic,
        Automatic,
    }

    public enum WeaponStatus
    {
        Permanent,
        Temporary,
    }
}

