using System.Collections;
using System.Net;
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
    public float ammoBar { get { return ammoProgressBar; } }
    public float maxAmmo { get { return maxAmmoClip; } }
    public float reloadingTimer { get { return reloadTimer; } }
    public bool isReloading { get { return reloadTime; } }
    #endregion

    #region Method Variables
    private float cooldownTimer = Mathf.Infinity;
    private float shootCooldown;
    public float reloadTimer;
    private float bulletsCount;
    private float maxAmmoClip;
    private float ammoProgressBar;
    private bool reloadTime;
    #endregion

    #region Unity shit
    private void Awake()
    {
        player = GetComponent<Player>();
        applySound = GetComponent<ApplySound>();

        bulletsCount = 0.0f;
        maxAmmoClip = weaponData.weaponClip;
        reloadTimer = weaponData.realoadTime;
    }

    private void Start()
    {
        currentShootPosition = shootPosition;
        shootCooldown = 1f / weaponData.shotsPerSecond;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        SetUIAmmoBar(bulletsCount, maxAmmoClip);
        Shoot();

        if (bulletsCount >= maxAmmoClip)
        {
            Reload();
        }
        else if (!reloadTime && bulletsCount >= 0.0f)
        {
            bulletsCount = bulletsCount - weaponData.activeReloadValue * Time.deltaTime;
        }
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
        if (bulletsCount < maxAmmoClip && !reloadTime)
        {
            bulletsCount += 1;
            cooldownTimer = 0; 
            GameObject newBullet = Instantiate(weaponData.bulletPrefab, currentShootPosition.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(weaponData.bulletSpeed, 0.0f);
            bulletParticle.Play();
            applySound.PlayRandomSound(shootClips);
        }
    }
    private void Reload()
    {
        reloadTime = true;
        reloadTimer -= Time.deltaTime;

        if (reloadTimer <= 0)
        {
            reloadTimer = weaponData.realoadTime;
            bulletsCount = 0.0f;
            reloadTime = false;
        }
    }

    private void SetUIAmmoBar(float bullCount, float maxCount)
    {
        ammoProgressBar = bullCount / maxCount * 100;
    }
    #endregion
}
