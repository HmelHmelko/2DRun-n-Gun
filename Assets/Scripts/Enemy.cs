using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float hp = 100.0f;

    private Animator enemyAnimator;

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(hp <= 0)
        {
            enemyAnimator.SetTrigger("Death");
           
            //Destroy(this.gameObject);
        }
    }
    public void Damage(float amount)
    {
        hp -= amount;
        enemyAnimator.SetTrigger("Hitted");

    }

}
