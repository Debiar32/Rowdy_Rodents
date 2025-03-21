using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Scr_LevelCompleter : MonoBehaviour
{
    public InputSystem_Actions inputSystem;
    private InputAction interact;

    [SerializeField] private int levelToComplete; // Choose level (0-3) in Inspector

    private void Awake()
    {
        inputSystem = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        interact = inputSystem.PlayerHub.HubInteraction;
        interact.Enable();
        interact.performed += CompleteLevel;
    }

    private void OnDisable()
    {
        interact.Disable();
    }

    public void CompleteLevel(InputAction.CallbackContext context)
    {
        // Ensure levelToComplete is within valid range (0 to 3) and map it to correct index (0 to 3)
        if (levelToComplete >= 0 && levelToComplete <= 3)
        {
            if (LevelProgressManager.instance != null)
            {
                LevelProgressManager.instance.levelsCompleted[levelToComplete] = true;
                Debug.Log("Level " + levelToComplete + " completed!");
            }
            else
            {
                Debug.LogError("LevelProgressManager instance is not found!");
            }

            GoBackToHub();
        }
        else
        {
            Debug.LogError("Invalid levelToComplete value. Must be between 0 and 3.");
        }
    }

    public void GoBackToHub()
    {
        SceneManager.LoadScene("Lvl_Hub");
    }
}
