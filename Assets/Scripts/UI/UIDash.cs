using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UIDash : MonoBehaviour
{
    [SerializeField] private Player player;
    private Slider dashSlider;
    public GameObject hui;
    public GameObject her;

    private void Awake()
    {
        dashSlider = GetComponent<Slider>();
    }
    private void Update()
    {
        CheckDashCD();

        if (dashSlider.value >= 1.0f)
        {
            hui.gameObject.SetActive(false);
            her.gameObject.SetActive(false);
        }
        else
        {
            hui.gameObject.SetActive(true);
            her.gameObject.SetActive(true);
        }
    }

    private void CheckDashCD()
    {
        dashSlider.value = player.dashCDValue;  
    }
}
