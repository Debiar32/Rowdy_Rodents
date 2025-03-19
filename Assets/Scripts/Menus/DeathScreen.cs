using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public static bool isReturningFromDeath = false;

    public GameObject returnToMainMenuButton;
    public GameObject deathScreenUI;

    void Start()
    {
        deathScreenUI.SetActive(false);
    }
    public void ShowDeathScreen()
    {
        deathScreenUI.SetActive(true);
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(returnToMainMenuButton);
    }

    public void RestartGame()
    {
        deathScreenUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitToMainMenu()
    {
        //deathScreenUI.SetActive(false);
        isReturningFromDeath = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("RR_Main_Menu");
        deathScreenUI.SetActive(false);
    }
}
