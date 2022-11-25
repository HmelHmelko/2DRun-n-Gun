using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    private List<IDamageable> detectedDamageable = new List<IDamageable>();

    public void AddToDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Debug.Log("add to dected" + damageable);
            detectedDamageable.Add(damageable);
        }
    }
    public void RemoveFromDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Debug.Log("remove to dected" + damageable);
            detectedDamageable.Remove(damageable);
        }
    }
    public void CheckShootDamage(float damage)
    {
        foreach (IDamageable item in detectedDamageable)
        {
            if (item != null)
            {
                item.Damage(damage);
            }
        }
    }
}
