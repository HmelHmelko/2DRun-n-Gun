using System.Collections;
using UnityEngine;

public class UIHealth : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject[] hearts;
    [SerializeField] private int pulseTimer;

    protected readonly int hashActivePara = Animator.StringToHash("Active");
    protected readonly int hashInactiveState = Animator.StringToHash("Inactive");
    protected readonly int hashPulseTrigger = Animator.StringToHash("Pulse");

    private int heartsCount;

    private void Awake()
    {
        heartsCount = hearts.Length;
    }

    private void Start()
    {
        StartCoroutine(CoroutinePulse());
    }

    private void Update()
    {

    }
    public void UpdateHealth()
    {
        heartsCount--;
        hearts[heartsCount].GetComponent<Animator>().SetBool(hashActivePara,false);
    }

    private IEnumerator CoroutinePulse()
    {
        while (true)
        {
            hearts[Random.Range(0, heartsCount)].GetComponent<Animator>().SetTrigger(hashPulseTrigger);

            yield return new WaitForSeconds(pulseTimer);
        }
    }
}
