using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;  // Import the new Input System

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public bool isPaused;
    public DeathScreen DeathScreenScript;  // Reference to the DeathScreen script

    public GameObject resumePauseButton;
    public static bool pauseToMenu = false;

    public InputSystem_Actions pauseScreenInputs;
    private InputAction pausing;
    private InputAction unPausing;


    void Start()
    {
        pauseMenuUI.SetActive(false);
    }


    private void Awake()
    {
        pauseScreenInputs = new InputSystem_Actions();
    }

 /*
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
 */

    private void OnEnable()
    {
        pausing = pauseScreenInputs.UI.PauseMenuOpen;
        pausing.Enable();
        pausing.performed += PauseGameButton;

        //unPausing = pauseScreenInputs.UI.PauseMenuClose;
        //unPausing.Enable();
        //unPausing.performed += ResumeGameButton;

    }

    private void OnDisable()
    {
        pausing.Disable();
        //unPausing.Disable();
    }



    public void PauseGameButton(InputAction.CallbackContext context)
    {
        
        if (!isPaused)
        {
            PauseGame();
        } 
        else
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void RestartGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumePauseButton);
    }

    public void ExitToMainMenu()
    {
        pauseToMenu = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("RR_Main_Menu");
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }
}
