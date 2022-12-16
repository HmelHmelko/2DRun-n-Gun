using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject spawnerEnemy;
    [SerializeField] Transform pos;

    Vector3 spawnPos;

    private void Awake()
    {
        spawnPos = new Vector3(pos.transform.position.x, pos.transform.position.y, pos.transform.position.z);
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        spawnPos = new Vector3(pos.transform.position.x, pos.transform.position.y, pos.transform.position.z);
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(spawnerEnemy, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(2.0f);
            Instantiate(spawnerEnemy, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(2.0f);
        }
    }


}
