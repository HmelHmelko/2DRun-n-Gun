using Cinemachine.Utility;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 5;
    [SerializeField] private int damage = 1;
    [SerializeField] private ApplySound applySound;

    private Damager damager;
    private Player player;
    private Animator enemyAnimator;
    private Collider2D enemyColl;
    private Rigidbody2D enemyRB;

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyColl = GetComponent<Collider2D>();
        enemyRB = GetComponent<Rigidbody2D>();
        damager = GetComponent <Damager>();
        applySound = GetComponent<ApplySound>();
    }

    private void Update()
    {
        if(hp <= 0)
        {
            EnemyDeath();
        }
    }
    public void Damage(int amount)
    {
        hp -= amount;
        enemyAnimator.SetTrigger("Hitted");
    }

    private void EnemyDeath()
    {
        enemyAnimator.SetTrigger("Death");
        enemyColl.enabled = false;
        enemyRB.gravityScale = 0;
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
