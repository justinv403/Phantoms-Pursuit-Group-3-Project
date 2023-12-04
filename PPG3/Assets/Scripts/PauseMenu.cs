using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public FirstPersonController cameraController;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

public void Resume()
{
    pauseMenuUI.SetActive(false);
    Time.timeScale = 1f;
    GameIsPaused = false;
    // Make sure the cursor is locked and hidden when resuming the game
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
    // Re-enable the camera controller if needed
     cameraController.enabled = true;
}

public void Pause()
{
    pauseMenuUI.SetActive(true);
    Time.timeScale = 0f;
    GameIsPaused = true;
    // Ensure the cursor is visible and not locked when the game is paused
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    // Optionally, disable the camera controller if needed
    cameraController.enabled = false;
}
}
