using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;  // Import the new Input System

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public bool isPaused;
    public DeathScreen DeathScreenScript;  // Reference to the DeathScreen script

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }


    void Update()
    {
        // Use the new Input System to check for the Escape key
        //if(Input.GetKeyDown(KeyCode.Escape))
        if(Keyboard.current.escapeKey.wasPressedThisFrame && !DeathScreenScript.deathScreenUI.activeSelf)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("RR_Main_Menu");
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }
}
