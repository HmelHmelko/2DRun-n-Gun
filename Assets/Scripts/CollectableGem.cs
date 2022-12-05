using UnityEngine;

public class CollectableGem : MonoBehaviour, ICollectable
{
    public void Collect(float amount)
    {
        Debug.Log("Collected gem " + amount);
        Destroy(gameObject);
    }
}
