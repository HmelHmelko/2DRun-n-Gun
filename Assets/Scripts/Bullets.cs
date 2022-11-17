using UnityEngine;

public class Bullets : MonoBehaviour
{
    private Rigidbody2D bulletRB2d;

    private void Awake()
    {
        bulletRB2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
    }
}
