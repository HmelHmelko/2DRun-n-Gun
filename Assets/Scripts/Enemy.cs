using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float hp = 100.0f;

    private Animator enemyAnimator;
    private Collider2D enemyColl;
    private Rigidbody2D enemyRB;

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyColl = GetComponent<Collider2D>();
        enemyRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(hp <= 0)
        {
            EnemyDeath();
        }
    }
    public void Damage(float amount)
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

}
