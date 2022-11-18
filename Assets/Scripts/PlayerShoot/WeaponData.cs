using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Base Data")]
public class WeaponData : ScriptableObject
{
    [Header("Shoot Settings")]
    public GameObject bulletPrefab;
    public float shotsPerSecond = 2.0f;
    public float bulletSpeed = 20.0f;

    [Header("Bullets Settings")]
    public float autoDestructTime = 5.0f;
    public float damage = 1.0f;

}
