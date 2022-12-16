using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 5;
    [SerializeField] private int damage = 1;
    //[SerializeField] private AudioClip deathClip;
    [SerializeField] GameObject shadow;
    public CameraShake cameraShake;

    public bool EnemyDead { get; private set; }

    public int EnemyHealth { get { return hp; } }

    //private AudioSource audioSource;
    private Damager damager;
    private Player player;
    private Animator enemyAnimator;
    private Collider2D enemyColl;
    private BoxCollider2D enemyBox;
    private Rigidbody2D enemyRB;

    public Vector2 currentVelocity { get; private set; }
    private Vector2 moveVector;

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyColl = GetComponent<Collider2D>();
        enemyBox = GetComponent<BoxCollider2D>();
        enemyRB = GetComponent<Rigidbody2D>();
        damager = GetComponent <Damager>();

        cameraShake = FindObjectOfType<CameraShake>();

        //audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        SetVelocity(-4);
        currentVelocity = enemyRB.velocity;

        if (EnemyHealth <= 0)
        {
            EnemyDeath();
        }
    }

    private void SetVelocity(float velocity)
    {
        moveVector.Set(velocity, currentVelocity.y);
        enemyRB.velocity = moveVector;
        currentVelocity = moveVector;
    }
    public void Damage(int amount)
    {
        hp -= amount;
        enemyAnimator.SetTrigger("Hitted");
    }
    private IEnumerator DeathDestroyDelay()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
    private void EnemyDeath()
    {
        //audioSource.PlayOneShot(deathClip);
        EnemyDead = true;
        enemyAnimator.SetTrigger("Death");
        shadow.SetActive(false);
        enemyColl.enabled = false;
        enemyBox.enabled = false;
        enemyRB.gravityScale = 0;
        StartCoroutine(DeathDestroyDelay());
    }

    public void CameraDeathTrigger()
    {
        cameraShake.HitShake();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if (player != null)
        {
            damager.AddToDetected(collision);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if (player != null)
        {
            damager.CheckDamage(damage);
            enemyColl.isTrigger = true;
            enemyRB.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();

        if (player != null)
        {
            damager.RemoveFromDetected(collision);
        }
    }

}
