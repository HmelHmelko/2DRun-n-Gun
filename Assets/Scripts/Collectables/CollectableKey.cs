using UnityEngine;
using UnityEngine.InputSystem;


public class CollectableKey : MonoBehaviour, IDamageable, ICollectable
{
    [SerializeField] GameObject keyPrefab;

    public Collider2D keyCollider { get; private set; }
    private Rigidbody2D keyRb;

    private int hp = 1;

    private void Awake()
    {
        keyCollider = GetComponent<Collider2D>();
        keyRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (hp <= 0)
        {

        }
    }

    public void Collect(float amount)
    {
        Debug.Log("Collected Key");
    }

    public void Damage(int amount)
    {
        hp -= amount;
        Debug.Log("You damaged KeyHoler");
        AfterDamage();
    }

    private void AfterDamage()
    {
        keyRb.gravityScale = 0;
        keyCollider.isTrigger = true;
    }

}
