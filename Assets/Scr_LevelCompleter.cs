using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Scr_LevelCompleter : MonoBehaviour
{
    public InputSystem_Actions inputSystem;
    public InputAction interact;

    [SerializeField] private int levelToComplete; // Choose level (1-4) in Inspector

    private void OnEnable()
    {
        interact = inputSystem.PlayerHub.Interact;
        interact.Enable();
        interact.performed += CompleteLevel;
    }

    private void OnDisable()
    {
        interact.Disable();
        interact.performed -= CompleteLevel;
    }

    private void CompleteLevel(InputAction.CallbackContext context)
    {
        if (LevelProgressManager.instance != null && levelToComplete >= 1 && levelToComplete <= 4)
        {
            LevelProgressManager.instance.levelsCompleted[levelToComplete - 1] = true;
            Debug.Log("Level " + levelToComplete + " completed!");
            GoBackToHub();
        }
    }

    public void GoBackToHub()
    {
        SceneManager.LoadScene("Lvl_00_Hub");
    }
}
