using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOverScreen : MonoBehaviour
{
    public bool gameOnPause { get; private set; }

    private void Awake()
    {
        gameOnPause = false;
    }
    public void EnableScreen()
    {
        Time.timeScale = 0.0f;
        gameObject.SetActive(true);
        gameOnPause = true;
    }

    public void DisableScreen()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        gameOnPause = false;
    }

    public void RestartButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("_TestScene", LoadSceneMode.Single);
        Time.timeScale = 1.0f;
    }

    public void ExitButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void BackButton()
    {
        DisableScreen();
    }

    public void OptionsButton()
    {

    }

}
