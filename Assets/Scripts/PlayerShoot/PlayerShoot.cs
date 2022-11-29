using System.Collections.Generic;
using UnityEngine;


public class PlayerShoot : MonoBehaviour
{
    #region References
    private Player player;
    [SerializeField] public WeaponData weaponData;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private ParticleSystem bulletParticle;
    [SerializeField] private ApplySound applySound;
    #endregion

    #region Components/Dynamic Fields/Interface
    public AudioClip[] shootClips;
    public Transform currentShootPosition { get; private set; }
    #endregion

    #region Method Variables
    private float cooldownTimer = Mathf.Infinity;
    private float shootCooldown;
    private float reloadTimer;
    private int bulletsCount;
    #endregion

    #region Unity shit
    private void Awake()
    {
        player = GetComponent<Player>();
        applySound = GetComponent<ApplySound>();
    }

    private void Start()
    {
        currentShootPosition = shootPosition;
        shootCooldown = 1f / weaponData.shotsPerSecond;

        reloadTimer = weaponData.realodTime;
        bulletsCount = weaponData.weaponClip;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        Shoot();
    }
    #endregion

    #region ShootLogic
    public void Shoot()
    {
        if (player.playerInputHandler.ShootInputs[(int)ShootInputsEnum.Primary] && cooldownTimer >= shootCooldown)
        {
            SpawnBullet();
        }
    }
    private void SpawnBullet()
    {
        if (bulletsCount > 0)
        {
            bulletsCount--;
            cooldownTimer = 0;
            GameObject newBullet = Instantiate(weaponData.bulletPrefab, currentShootPosition.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(weaponData.bulletSpeed, 0.0f);
            bulletParticle.Play();
            applySound.PlayRandomSound(shootClips);
        }
        else if(bulletsCount == 0)
        {
            Reload();
        }
    }

    private void Reload()
    {
        reloadTimer -= Time.deltaTime;
        if(reloadTimer <= 0)
        {
            reloadTimer = weaponData.realodTime;
            bulletsCount = weaponData.weaponClip;
        }
    }
    #endregion
}
