using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenuButton : MonoBehaviour
{
    public void PlayButtonOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

}
