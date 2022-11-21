using UnityEngine;

public class Crate : MonoBehaviour, IDamageable
{
    public void Damage(float amount)
    {
        Debug.Log(amount + "U brake me, bro");
    }

}
