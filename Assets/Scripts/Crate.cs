using UnityEngine;

public class Crate : MonoBehaviour, IDamageable
{
    private int hp = 1;
    [SerializeField] GameObject gemPrefab;

    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void Damage(int amount)
    {
        hp -= amount;
    }

    public void OnDestroy()
    {
        Instantiate(gemPrefab,transform.localPosition,Quaternion.identity);
    }

}
