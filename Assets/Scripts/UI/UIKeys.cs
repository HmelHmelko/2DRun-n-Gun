using UnityEngine;

public class UIKeys : MonoBehaviour
{
    [SerializeField] GameObject[] keys;

    private int keysCount;

    protected readonly int hashActivePara = Animator.StringToHash("Active");

    private void Awake()
    {
        keysCount = 0;
    }

    public void UpdateKeys()
    {
        keys[keysCount].GetComponent<Animator>().SetBool(hashActivePara, true);
        keysCount++;
    }
}
