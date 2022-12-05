using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UIDash : MonoBehaviour
{
    [SerializeField] private Player player;
    private Slider dashSlider;
    public GameObject backGround;
    public GameObject fillArea;

    private float blinkTimer = 2f;
    private float blinkCounterTimer = 0.0f;

    private void Awake()
    {
        dashSlider = GetComponent<Slider>();
    }
    private void Update()
    {
        CheckDashCD();

        if (dashSlider.value >= 1.0f)
        {
            blinkCounterTimer += Time.deltaTime;
            backGround.gameObject.SetActive(false);
            fillArea.gameObject.SetActive(false);
        }
        else
        {
            blinkCounterTimer = 0.0f;
            backGround.gameObject.SetActive(true);
            fillArea.gameObject.SetActive(true);
        }
    }

    private void CheckDashCD()
    {
        dashSlider.value = player.dashCDValue;  
    }

}
