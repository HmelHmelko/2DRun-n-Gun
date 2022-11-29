using UnityEngine;

public class Crate : MonoBehaviour, IDamageable
{
    public void Damage(int amount)
    {
        Debug.Log(amount + "U brake me, bro");
    }

}
