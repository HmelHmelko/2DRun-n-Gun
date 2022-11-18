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
    #region Components/Dynamic Fields
    public AudioClip[] shootClips;
    public Transform currentShootPosition { get; private set; }
    #endregion
    #region Method Variables
    private float cooldownTimer = Mathf.Infinity;
    private float shootCooldown;
    #endregion

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
    }

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
}
