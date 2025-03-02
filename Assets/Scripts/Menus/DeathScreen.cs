using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject deathScreenUI;

    void Start()
    {
        deathScreenUI.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            // If the death screen is not active, set it to active
            if (!deathScreenUI.activeSelf)
            {
                deathScreenUI.SetActive(true);
            }
        }
    }

    public void ShowDeathScreen()
    {
        deathScreenUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
