using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneGameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] UIGameOverScreen gameOverScreen;
    Cursor cursor;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CursorHide();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameOverScreen.gameOnPause)
            {
                gameOverScreen.DisableScreen();
            }
            else
            {
                gameOverScreen.EnableScreen();
            }
        }

        if (player.Health <= 0)
        {
            gameOverScreen.EnableScreen();
        }

    }

    private void CursorHide()
    {
        if(gameOverScreen.gameOnPause)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if(!gameOverScreen.gameOnPause)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}
