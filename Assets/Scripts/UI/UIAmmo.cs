using ExtrasClipperLib;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class UIAmmo : MonoBehaviour
{
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private CameraShake cameraShake;
    private Slider mainAmmoSlider;

    private float minAmmoValue;
    private float maxAmmoValue;
    private float velocity = 0.0f;
    private void Awake()
    {
        mainAmmoSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        minAmmoValue = 0.0f;
        maxAmmoValue = 100.0f;

        mainAmmoSlider.minValue = minAmmoValue;
        mainAmmoSlider.maxValue = maxAmmoValue;
    }

    private void Update()
    {
        AmmoCheck();
    }

    public void AmmoCheck()
    {
        minAmmoValue = 0.0f;
        maxAmmoValue = 100.0f;
        float currentAmmo = Mathf.SmoothDamp(mainAmmoSlider.value, playerShoot.ammoBar, ref velocity, 20 * Time.deltaTime);
        mainAmmoSlider.value = currentAmmo;
    }
}
