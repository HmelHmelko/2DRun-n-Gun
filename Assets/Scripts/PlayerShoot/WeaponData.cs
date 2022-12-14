using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Base Data")]
public class WeaponData : ScriptableObject
{
    [Header("Shoot Settings")]
    public GameObject bulletPrefab;
    public float shotsPerSecond = 2.0f;
    public float bulletSpeed = 20.0f;
    public float weaponClip = 15;
    public float realoadTime = 2.5f;
    public float activeReloadValue = 1.0f;

    [Header("Bullets Settings")]
    public float autoDestructTime = 5.0f;
    public int weaponDamage = 1;

}
