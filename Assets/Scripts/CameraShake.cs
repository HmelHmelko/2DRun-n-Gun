using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] Animator animator;

    protected readonly int hashShakeTrigger = Animator.StringToHash("GetHitShake");
    protected readonly int hashShootingShakeTrigger = Animator.StringToHash("Shooting");
    protected readonly int hashEnemyDeathShakeTrigger = Animator.StringToHash("EnemyDeath");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HitShake()
    {
        animator.SetTrigger(hashShakeTrigger);
    }

    public void ShootingShake()
    {
        animator.SetTrigger(hashShootingShakeTrigger);
    }

    public void EnemyDeathShake()
    {
        animator.SetTrigger(hashEnemyDeathShakeTrigger);
    }
}
