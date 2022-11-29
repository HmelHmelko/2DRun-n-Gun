using UnityEngine;

public class UIHealth : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject[] hearts;

    protected readonly int m_HashActivePara = Animator.StringToHash("Active");
    protected readonly int m_HashInactiveState = Animator.StringToHash("Inactive");

    private int heartsCount;

    private void Awake()
    {
        heartsCount = hearts.Length;
    }

    public void UpdateHealth()
    {
        heartsCount--;
        hearts[heartsCount].GetComponent<Animator>().SetBool(m_HashActivePara,false);
    }

    public void ChangeHitPointUI()
    {

    }
}
