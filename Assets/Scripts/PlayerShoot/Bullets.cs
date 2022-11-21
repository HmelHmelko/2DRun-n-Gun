using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private PlayerShoot playerShoot;
    private Rigidbody2D bulletRB2d;
    private Animator bulletAnimator;
    private CapsuleCollider2D capsuleCollider;

    private float bulletStartTime;

    private void Awake()
    {
        bulletRB2d = GetComponent<Rigidbody2D>();
        bulletAnimator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        bulletStartTime = Time.time;
    }
    private void Update()
    {
        AutoDestruct();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        Debug.Log(collision.gameObject);
        playerShoot.AddToDetected(collision);
        playerShoot.CheckShootDamage();
        BulletHitTrigger();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerShoot.RemoveFromDetected(collision);
    }
    private void AutoDestruct()
    {
        if(Time.time >= bulletStartTime + weaponData.autoDestructTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void BulletHitTrigger()
    {
        capsuleCollider.enabled = false;
        bulletRB2d.velocity = Vector3.zero;
        bulletAnimator.SetTrigger("hitHappen");
    }

}
