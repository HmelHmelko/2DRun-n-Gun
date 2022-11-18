using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] private WeaponData weaponData;
    private Rigidbody2D bulletRB2d;
    private Animator bulletAnimator;
    private CapsuleCollider2D capsuleCollider;

    private float bulletStartTime;
    private bool hitHappen;

    private void Awake()
    {
        bulletRB2d = GetComponent<Rigidbody2D>();
        bulletAnimator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        hitHappen = false;
        bulletStartTime = Time.time;
    }
    private void Update()
    {
        AutoDestruct();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject);
        hitHappen = true;
        capsuleCollider.enabled = false;
        bulletAnimator.SetTrigger("hitHappen");
        Destroy(this.gameObject);
    }

    private void AutoDestruct()
    {
        if(Time.time >= bulletStartTime + weaponData.autoDestructTime)
        {
            Destroy(this.gameObject);
        }
    }

}
