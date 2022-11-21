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
    private List<IDamageable> detectedDamageable = new List<IDamageable>();
    public AudioClip[] shootClips;
    public Transform currentShootPosition { get; private set; }
    #endregion

    #region Method Variables
    private float cooldownTimer = Mathf.Infinity;
    private float shootCooldown;
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
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        Shoot();

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log(detectedDamageable.Count);
        }
    }
    #endregion

    #region IDamageable logic implement
    public void AddToDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        //Debug.Log("Detected");

        if (damageable != null)
        {
            //Debug.Log("Add to Detected", collision.gameObject);
            detectedDamageable.Add(damageable);
        }
    }
    public void RemoveFromDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            //Debug.Log("Remove from detected", collision.gameObject);
            detectedDamageable.Remove(damageable);
        }
    }
    public void CheckShootDamage()
    {
        foreach (IDamageable item in detectedDamageable)
        {
            Debug.Log(item);
            item.Damage(weaponData.weaponDamage);
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
        cooldownTimer = 0;
        GameObject newBullet = Instantiate(weaponData.bulletPrefab, currentShootPosition.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(weaponData.bulletSpeed, 0.0f);
        bulletParticle.Play();
        applySound.PlayRandomSound(shootClips);
    }
    #endregion
}
