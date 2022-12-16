using UnityEngine;
using UnityEngine.UIElements;

public class Crate : MonoBehaviour, IDamageable
{
    private int hp = 1;
    [SerializeField] GameObject gemPrefab;
    [SerializeField] Transform spawn;

    private Vector3 spawnPos;
    private void Start()
    {
    }

    private void Update()
    {
        spawnPos = new Vector3(spawn.transform.position.x, spawn.transform.position.y, 0);

        if (hp <= 0)
        {
            Destruct();
            Destroy(this.gameObject);
        }
    }
    public void Damage(int amount)
    {
        hp -= amount;
    }

    private void Destruct()
    {
        Instantiate(gemPrefab, spawnPos, Quaternion.identity);
    }
}
