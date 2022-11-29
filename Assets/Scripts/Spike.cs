using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private Player player;
    private Damager damager;

    private void Awake()
    {
        damager = GetComponent<Damager>();
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
