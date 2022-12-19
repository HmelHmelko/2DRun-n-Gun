using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMapButtons : MonoBehaviour
{
    public void PlayPlanetButtonOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1.0f;
    }
}
